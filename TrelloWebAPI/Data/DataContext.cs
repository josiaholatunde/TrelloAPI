using Microsoft.EntityFrameworkCore;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {}
        public DbSet<User> Users { get; set; }
        public DbSet<BookingSubject> BookingSubjects { get; set; }
        public DbSet<GalleryPicture> GalleryPictures { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /*  modelBuilder.Entity<Comment>()
                        .HasKey(k => new { k.BookingSubjectId, k.UserId}); */
               modelBuilder.Entity<BookingSubject>()
                .HasOne(u => u.User)
                .WithMany(u => u.BookingSubjects)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Message>()
                            .HasOne(m => m.Sender)
                            .WithMany(m => m.MessagesSent)
                            .HasForeignKey(m => m.SenderId)
                            .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Message>()
                            .HasOne(m => m.Recipient)
                            .WithMany(m => m.MessagesReceived)
                            .HasForeignKey(m => m.RecipientId)
                            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}