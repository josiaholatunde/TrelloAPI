using TrelloWebAPI.Models;

namespace TrelloWebAPI.DTO
{
    public class UserToReturnDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public UserRole UserRole { get; set; }
    }
}