using System;

namespace TrelloWebAPI.DTO
{
    public class MessageToReturnDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderKnownAs { get; set; }
        public string SenderPhotoUrl { get; set; }
        public int RecipientId { get; set; }
        public string RecipientKnownAs { get; set; }
        public string RecipientPhotoUrl { get; set; }

        public string MessageContent { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime DateSent { get; set; }
    }
}