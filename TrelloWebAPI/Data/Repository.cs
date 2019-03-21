using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloWebAPI.Helpers;
using TrelloWebAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace TrelloWebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add<T>(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove<T>(entity);
        }

        public async Task<BookingSubject> GetBooking(int id)
        {
            var bookingSubject = await _context.BookingSubjects
                                    .Include(b => b.GalleryPictures)
                                    .Include(b => b.Features)
                                   .FirstOrDefaultAsync(bs => bs.Id == id);
            return bookingSubject;
        }

        public async Task<PagedList<BookingSubject>> GetBookings(SearchParams searchparams)
        {
            var bookingSubject = _context.BookingSubjects
                                            .Include(b => b.Comments)
                                                .ThenInclude(m => m.User)
                                            .Include(b => b.Features)
                                            .Include(b => b.GalleryPictures)
                                            .AsQueryable();
            //bookingSubject = bookingSubject.Where(bk => bk.Comments.Count > 0);
            if (!String.IsNullOrEmpty(searchparams.NameOrLocation))
            {
                bookingSubject = bookingSubject.Where(b => b.Name.StartsWith(searchparams.NameOrLocation) || b.City.StartsWith(searchparams.NameOrLocation) || b.Country.StartsWith(searchparams.NameOrLocation));
            }

            if (searchparams.BookingType == BookingSubjectType.CarRental)
                bookingSubject = bookingSubject.Where(b => b.BookingType == BookingSubjectType.CarRental);
            else if (searchparams.BookingType == BookingSubjectType.Tour)
                bookingSubject = bookingSubject.Where(b => b.BookingType == BookingSubjectType.Tour);
            else if (searchparams.BookingType == BookingSubjectType.Flight)
                bookingSubject = bookingSubject.Where(b => b.BookingType == BookingSubjectType.Flight);
            else
                bookingSubject = bookingSubject.Where(b => b.BookingType == BookingSubjectType.Hotel);

            
            bookingSubject = bookingSubject.Where(b => b.GalleryPictures.Count >= 3);
            return await PagedList<BookingSubject>.CreateAsync(bookingSubject, searchparams.PageSize, searchparams.PageNumber);
        }

        public async Task<Comment> GetComment(int id, int bookingId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(cm => cm.Id == id && cm.BookingSubjectId == bookingId);
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetComments(int bookingId)
        {
           var comments = await _context.Comments
                                        .Include(m => m.User)
                                        .Where(cm => cm.BookingSubjectId == bookingId).ToListAsync();
            return comments;
        }

        public async Task<GalleryPicture> GetGalleryPhoto(int bookingId, int photoId)
        {
            var galleryPhoto = await _context.GalleryPictures.FirstOrDefaultAsync(ph => ph.Id == photoId);
            return galleryPhoto;
        }

        public async Task<IEnumerable<GalleryPicture>> GetGalleryPhotos(int bookingId)
        {
            var galleryPhotos = await _context.GalleryPictures
                                                .Where(ph => ph.BookingSubjectId == bookingId)
                                                .ToListAsync();
            return galleryPhotos;
        }

        public async Task<Message> GetMessage(int messageId)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
            return message;
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Messages
                                    .Include(m => m.Sender)
                                        .ThenInclude(u => u.Photo)
                                    .Include(m => m.Recipient)
                                        .ThenInclude(u => u.Photo)
                                    .AsQueryable();
            switch(messageParams.MessageContainer)
            {
                case "Inbox": 
                        messages = messages.Where(m => m.RecipientId == messageParams.UserId && m.RecipientDeleted == false);
                        break;
                case "Outbox":
                        messages = messages.Where(m => m.SenderId == messageParams.UserId && m.SenderDeleted == false);
                        break;
                default: 
                        messages = messages.Where(m => m.RecipientId == messageParams.UserId && m.IsRead == false);
                        break;
            }
            messages = messages.OrderByDescending(m => m.DateSent);
            return await PagedList<Message>.CreateAsync(messages, messageParams.PageSize, messageParams.PageNumber);
        }

        public async Task<int> GetUnreadNotificationsCount(int userId)
        {
            var notificationCount = await _context.Messages.Where(m => m.RecipientId == userId && m.IsRead == false).ToListAsync();
            return notificationCount.Count;
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int senderId, int recipientId)
        {
           var messages = await _context.Messages
                                    .Include(m => m.Sender)
                                        .ThenInclude(u => u.Photo)
                                    .Include(m => m.Recipient)
                                        .ThenInclude(u => u.Photo)
                                    .Where(m => m.SenderId == senderId && m.RecipientId == recipientId
                                    || (m.RecipientId == senderId && m.SenderId == recipientId)).ToListAsync();
            return messages;
                                    
                                    
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await  _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<IEnumerable<BookingSubject>> GetUserBookings(int userId)
        {
           var userBookings =await _context.BookingSubjects
                                        .Include(bk => bk.GalleryPictures)
                                        .Where(bk => bk.UserId == userId)
                                        .ToListAsync();
            return userBookings;
        }

        public async Task<Photo> GetUserPhoto(int userId, int photoId)
        {
           var photoFromRepo = await _context.Photos.FirstOrDefaultAsync(ph => ph.Id == photoId && ph.UserId ==userId);
           return photoFromRepo;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
        }

        public async Task<bool> ValidateBookingExists(BookingSubjectType type)
        {
           if (await _context.BookingSubjects.AnyAsync(b => b.BookingType == type))
                return true;
            return false;
        }
    }
}