using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloWebAPI.Helpers;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.Data
{
    public interface IRepository
    {
        Task<PagedList<BookingSubject>> GetBookings(SearchParams searchparams);
        Task<BookingSubject> GetBooking(int id);
        Task<GalleryPicture> GetGalleryPhoto(int bookingId, int photoId);
        Task<Photo> GetUserPhoto(int userId, int photoId);
        Task<IEnumerable<GalleryPicture>> GetGalleryPhotos(int bookingId);
        Task<Comment> GetComment(int id, int bookingId);
        Task<IEnumerable<Comment>> GetComments(int bookingId);
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<bool> ValidateBookingExists(BookingSubjectType type);
        Task<User> GetUser(int userId);
        Task<IEnumerable<BookingSubject>> GetUserBookings(int userId);

        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        Task<Message> GetMessage(int messageId);
        Task<IEnumerable<Message>> GetMessageThread(int senderId, int recipientId);
        Task<int> GetUnreadNotificationsCount(int userId);
    }
}