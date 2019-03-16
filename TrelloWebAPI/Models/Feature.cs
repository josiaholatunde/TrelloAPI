namespace TrelloWebAPI.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public BookingSubject BookingSubject { get; set; }
        public int BookingSubjectId { get; set; }
    }
}