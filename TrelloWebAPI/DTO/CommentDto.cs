using System;

namespace TrelloWebAPI.DTO
{
    public class CommentDto
    {
        public string Description { get; set; }
        public DateTime DateCommented { get; set; }
        public double Rating { get; set; }
        public int UserId { get; set; }
        public int BookingSubjectId { get; set; }

        public CommentDto()
        {
            DateCommented = DateTime.Now;
        }
    }
}