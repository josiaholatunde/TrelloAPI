using System.ComponentModel.DataAnnotations;

namespace TrelloWebAPI.DTO
{
    public class UserForLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8, MinimumLength= 4)]
        public string Password { get; set; }

    }
}