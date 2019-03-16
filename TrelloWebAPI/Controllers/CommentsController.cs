using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrelloWebAPI.Data;
using TrelloWebAPI.DTO;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.Controllers
{
   [Route("api/bookings/{bookingId}/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public CommentsController(IRepository repository, IAuthRepository authRepository,IMapper mapper)
        {
            _repository = repository;
            _authRepository = authRepository;
            _mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetComments(int bookingId)
        {
            var comments = await _repository.GetComments(bookingId);
            var commentToReturn = _mapper.Map<IEnumerable<CommentToReturnDto>>(comments);
            return Ok(commentToReturn);
        }
        [HttpGet("{id}", Name="GetComment")]
        public async Task<IActionResult> GetComment(int id, int bookingId)
        {
            var comment = await _repository.GetComment(id, bookingId);
            return Ok(comment);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(int bookingId, [FromBody] CommentDto commentDto)
        {
            if(commentDto.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _repository.GetUser(commentDto.UserId);
            if (await _authRepository.UserExists(userFromRepo.UserName))
                return Unauthorized();
            var booking = await _repository.GetBooking(bookingId);
            if (booking == null)
                return BadRequest("Booking does not exist");
            commentDto.BookingSubjectId = bookingId;
            var commentToCreate = _mapper.Map<CommentDto, Comment>(commentDto);
            booking.Comments.Add(commentToCreate);
            if (await _repository.SaveAll())
            {
                var commentToReturn = _mapper.Map<CommentToReturnDto>(commentToCreate);
                return CreatedAtRoute("GetComment", new { id = commentToReturn.Id}, commentToReturn);
            }

            return BadRequest("Error ocurred while creating comment");
        }

    }
}