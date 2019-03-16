using System;

namespace TrelloWebAPI.DTO
{
    public class PhotoToReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
    }
}