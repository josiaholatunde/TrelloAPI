using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrelloWebAPI.Data;
using TrelloWebAPI.DTO;
using TrelloWebAPI.Helpers;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    public class MessageController: ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public MessageController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("{id}", Name="GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var messageFromRepo = await _repository.GetMessage(id);
            if (messageFromRepo == null)
                return NotFound();
            return Ok(messageFromRepo);

        }
        [HttpGet("unread")]
        public async Task<IActionResult> GetUnreadNotificationCount(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var count = await _repository.GetUnreadNotificationsCount(userId);
            return Ok(count);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _repository.GetUser(recipientId);
            if (userFromRepo == null)
                return BadRequest("Recipient does not exist");
            var thread = await _repository.GetMessageThread(userId, recipientId);
            var messageToReturn = _mapper.Map<IEnumerable<MessageToReturnDto>>(thread);
            return Ok(messageToReturn);

        }
        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userId, [FromQuery] MessageParams messageParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            messageParams.UserId = userId;
            var messagesFromRepo = await _repository.GetMessagesForUser(messageParams);
            Response.AddPagination(messagesFromRepo.CurrentPage, messagesFromRepo.PageSize, messagesFromRepo.TotalPages, messagesFromRepo.TotalCount);
            var messageToReturn = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);
            return Ok(messageToReturn);
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, [FromBody] MessageForCreationDto messageForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            messageForCreationDto.SenderId = userId;
            var recipientFromRepo = await _repository.GetUser(messageForCreationDto.RecipientId);
            if (recipientFromRepo == null)
                return BadRequest("Recipient does not exist");
            var messageToCreate = _mapper.Map<Message>(messageForCreationDto);
            _repository.Add(messageToCreate);
            if (await _repository.SaveAll())
            {
                var messageToReturn = _mapper.Map<MessageForCreationDto>(messageToCreate);
                return CreatedAtRoute("GetMessage", new { id = messageToReturn.Id}, messageToReturn);
            }
            throw new Exception("Error Occurred while saving message");
        }
        
    }
}