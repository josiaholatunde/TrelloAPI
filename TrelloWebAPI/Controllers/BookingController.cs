using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrelloWebAPI.Data;
using TrelloWebAPI.DTO;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public BookingsController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
         // GET api/values
        [HttpGet("myBookings/{userId}")]
        public async Task<IActionResult> GetMyBookings(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var myBookings = await _repository.GetUserBookings(userId);
            var myBookingsToReturn = _mapper.Map<IEnumerable<BookingSubject>, IEnumerable<BookingSubjectToReturn>>(myBookings);
            return Ok(myBookingsToReturn);
            
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetBookings([FromQuery] SearchParams searchParams)
        {
            var bookingFromRepo = await _repository.GetBookings(searchParams);
            var bookingToReturn = _mapper.Map<IEnumerable<BookingSubject>, IEnumerable<BookingSubjectToReturn>>(bookingFromRepo);
            return Ok(bookingToReturn);
        }

        // GET api/values/5
        [HttpGet("{id}", Name="GetBooking")]
        public async Task<IActionResult> Get(int id)
        {
            var booking = await _repository.GetBooking(id);
            var bookingToReturn = _mapper.Map<BookingSubjectForEditDetail>(booking);
            return Ok(bookingToReturn);
        }

        // POST api/values
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBooking(int userId, [FromBody]BookingToCreateDto bookingForCreation)
        {
            if (bookingForCreation.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _repository.GetUser(bookingForCreation.UserId);
            if (userFromRepo.UserRole != UserRole.Admin)
                return Unauthorized();
            if (!ValidateBookingExists(bookingForCreation.BookingType))
                return BadRequest("Booking type does not exist");
            
            var bookingToCreate = _mapper.Map<BookingToCreateDto, BookingSubject>(bookingForCreation);
            userFromRepo.BookingSubjects.Add(bookingToCreate);
            if (await _repository.SaveAll())
            {
                var bookingToReturn = _mapper.Map<BookingToCreateDto>(bookingToCreate);
                return CreatedAtRoute("GetBooking", new { id = bookingToReturn.Id}, bookingToReturn);
            }
            return BadRequest("Error in creating Booking");

        }
        [Authorize]
        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody]BookingToCreateDto bookingForUpdateDto)
        {
            if (bookingForUpdateDto.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _repository.GetUser(bookingForUpdateDto.UserId);
            if (userFromRepo.UserRole != UserRole.Admin)
                return Unauthorized();
            if (!ValidateBookingExists(bookingForUpdateDto.BookingType))
                return BadRequest("Booking type does not exist");
            var bookingFromRepo = await _repository.GetBooking(bookingId);
           var bookingToUpdate = _mapper.Map<BookingToCreateDto, BookingSubject>(bookingForUpdateDto,bookingFromRepo);
           _repository.Update(bookingToUpdate);
           if (await _repository.SaveAll())
           {
               return NoContent();
           }
           return BadRequest("Error occurred while updating booking");

        }
         [Authorize]
        [HttpDelete("{bookingId}/{userId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _repository.GetUser(userId);
            if (userFromRepo.UserRole != UserRole.Admin)
                return Unauthorized();
            var bookingFromRepo = await _repository.GetBooking(bookingId);
            if (bookingFromRepo == null)
                return BadRequest("Booking does not exist");
            _repository.Delete(bookingFromRepo);
            if (await _repository.SaveAll())
                return NoContent();
            return BadRequest($"Error occurred while deleting Booking with id {bookingId}");
        }

        private bool ValidateBookingExists(BookingSubjectType type)
        {
           if (Enum.IsDefined(typeof(BookingSubjectType), type))
                return true;
            return false;
        }

    }
}
