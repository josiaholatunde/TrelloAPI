using System;
using Microsoft.AspNetCore.Http;

namespace TrelloWebAPI.DTO
{
    public class PhotoToCreateDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public int UserId { get; set; }
        public PhotoToCreateDto()
        {
            DateAdded = DateTime.Now;
        }

    }
}