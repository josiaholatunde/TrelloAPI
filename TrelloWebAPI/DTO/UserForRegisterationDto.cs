using System.ComponentModel.DataAnnotations;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.DTO
{
    public class UserForRegisterationDto
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8, MinimumLength= 4)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole UserRole { get; set; }

    }
}