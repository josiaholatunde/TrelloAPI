using System.Collections.Generic;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.DTO
{
    public class BookingToCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public Location Location { get; set; }
        public int NoOfVoters { get; set; }
        public Description Description { get; set; }
        public int NoOfBookingSubjectsLeft { get; set; }
        public int TotalBookingSubjects { get; set; }
        public BookingSubjectType BookingType { get; set; }
        public ICollection<string> Features { get; set; }


    }
}