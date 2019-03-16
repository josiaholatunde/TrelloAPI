using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrelloWebAPI.DTO;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> RegisterUser(User user, string passsword)
        {
            byte[] passwordHash, passwordSalt;
            ComputePasswordHash(out passwordHash, out passwordSalt, passsword);
            user.UserName = user.UserName.ToLower();
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void ComputePasswordHash(out byte[] passwordHash, out byte[] passwordSalt, string passsword)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passsword));
            }
        }

        
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(b => b.UserName == username))
                return false;
            return true; 
        }

        public async Task<User> LoginUser(UserForLoginDto user)
        {
            var userFromRepo = await _context.Users
                                            .Include(u => u.Photo)
                                            .FirstOrDefaultAsync(u => u.UserName == user.UserName.ToLower());
            if (userFromRepo == null)
                return null;
            if (!VerifyPasswordHash(user.Password, userFromRepo.PasswordHash, userFromRepo.PasswordSalt))
                return null;
            return userFromRepo;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedPasswordHash.Length; i++)
                {
                    if (computedPasswordHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }
    }
}