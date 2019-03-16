using System.Threading.Tasks;
using TrelloWebAPI.DTO;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.Data
{
    public interface IAuthRepository
    {
        Task<User> RegisterUser(User user, string passsword);
        Task<User> LoginUser(UserForLoginDto user);
        Task<bool> UserExists(string username);

    }
}