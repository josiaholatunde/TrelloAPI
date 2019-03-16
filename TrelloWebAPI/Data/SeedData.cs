using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.Data
{
    public class SeedData
    {
        private readonly DataContext _context;

        public SeedData(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
           if (!_context.Users.Any())
           {
                var userData = System.IO.File.ReadAllText("Data/UserSeed.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    ComputePasswordHash("password", out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.UserName = user.UserName.ToLower();
                    _context.Users.Add(user);
                }
                _context.SaveChanges();
           }
        }

        private void ComputePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public void SeedBookings()
        {
            if (!_context.BookingSubjects.Any())
            {
                var bookingData = System.IO.File.ReadAllText("Data/BookingSubjectSeed.json");
                var bookings = JsonConvert.DeserializeObject<List<BookingSubject>>(bookingData);
                foreach (var booking in bookings)
                {
                    _context.Add(booking);
                    _context.SaveChanges();
                }
            }
        }

        public void SeedUserComments()
        {
            if (!_context.Comments.Any())
            {
                var commentsData = System.IO.File.ReadAllText("Data/UserCommentsSeed.json");
                var comments = JsonConvert.DeserializeObject<List<Comment>>(commentsData);
                foreach (var comment in comments)
                {
                    _context.Comments.Add(comment);
                    _context.SaveChanges();
                }
            }
        }
    }
}