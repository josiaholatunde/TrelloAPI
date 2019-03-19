using Microsoft.AspNetCore.Http;

namespace TrelloWebAPI.Models
{
    public class GalleryPicture
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public BookingSubject BookingSubject { get; set; }
        public int BookingSubjectId { get; set; }
        public string PublicId { get; set; }

        
    }

}