using System;

namespace TrelloWebAPI.DTO
{
    public class CommentToReturnDto
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public DateTime DateCommented { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }


        public double Rating { get; set; }
        public int UserId { get; set; }
        public int BookingSubjectId { get; set; }

        public CommentToReturnDto()
        {
            DateCommented = DateTime.Now;
        }
    }
}