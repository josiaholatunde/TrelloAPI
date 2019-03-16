using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TrelloWebAPI.Models
{
    public class BookingSubject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int NoOfVoters { get; set; }
        public string MainDescription { get; set; }
        public string SubDescription { get; set; }
        public int NoOfBookingSubjectsLeft { get; set; }
        public int TotalBookingSubjects { get; set; }
        public DateTime CreatedDate { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public BookingSubjectType BookingType { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Feature> Features { get; set; }
        public ICollection<GalleryPicture> GalleryPictures { get; set; }
        public BookingSubject()
        {
            Comments = new Collection<Comment>();
            Features = new Collection<Feature>();
            GalleryPictures = new Collection<GalleryPicture>();
        }

    }

}