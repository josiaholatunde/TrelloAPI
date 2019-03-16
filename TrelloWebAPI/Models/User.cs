using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TrelloWebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public Photo Photo { get; set; }
        public UserRole UserRole { get; set; }
        public ICollection<BookingSubject> BookingSubjects { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public User()
        {
            BookingSubjects =  new Collection<BookingSubject>();
            Comments = new Collection<Comment>();
            MessagesSent = new Collection<Message>();
            MessagesReceived = new Collection<Message>();
        }
    }
}