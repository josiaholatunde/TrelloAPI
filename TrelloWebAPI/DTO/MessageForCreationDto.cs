using System;

namespace TrelloWebAPI.DTO
{
    public class MessageForCreationDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string MessageContent { get; set; }
        public DateTime DateSent { get; set; }
        public MessageForCreationDto()
        {
            DateSent = DateTime.Now;
        }

    }
}