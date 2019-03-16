using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TrelloWebAPI.Data;
using TrelloWebAPI.DTO;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repository, IMapper mapper, IConfiguration config)
        {
            _repository = repository;
            _mapper = mapper;
            _config = config;
        }
        // GET api/values
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegisterationDto userForRegisterationDto)
        {
            if (string.IsNullOrEmpty(userForRegisterationDto.UserName) && string.IsNullOrEmpty(userForRegisterationDto.Password))
                return BadRequest("Invalid Registeration Credentials");
            if (! await _repository.UserExists(userForRegisterationDto.UserName.ToLower()))
                return BadRequest("Username exists. Try again with another username");
            var userToRegister = _mapper.Map<User>(userForRegisterationDto);
            var createdUser = await _repository.RegisterUser(userToRegister, userForRegisterationDto.Password);
            var userToReturn = _mapper.Map<UserToReturnDto>(userToRegister);
            //return CreatedAtRoute("GetUser", new { controller="User", id = createdUser.Id }, userToReturn);
            return Ok(userToReturn);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            if (string.IsNullOrEmpty(userForLoginDto.UserName) && string.IsNullOrEmpty(userForLoginDto.Password))
                return BadRequest("Invalid Login Credentials");
            var userFromRepo = await _repository.LoginUser(userForLoginDto);
            if (userFromRepo == null)
                return Unauthorized();
            var claims = new [] {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signinCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToReturn = _mapper.Map<UserToReturnDto>(userFromRepo);
            return Ok(new {
                token = tokenHandler.WriteToken(token),
                user = userToReturn
            });        
        }    
        
    }
}