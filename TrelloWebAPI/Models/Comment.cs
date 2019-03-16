using System;

namespace TrelloWebAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateCommented { get; set; }
        public double Rating { get; set; }
        public bool IsRecommended { get; set; }
        public BookingSubject BookingSubject { get; set; }
        public int BookingSubjectId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}