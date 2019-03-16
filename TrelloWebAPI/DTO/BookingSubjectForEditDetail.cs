using System;
using System.Collections.Generic;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.DTO
{
    public class BookingSubjectForEditDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int AvgRating { get; set; }
        public int NoOfVoters { get; set; }
        public int noOfRecommendations { get; set; }
        public DateTime CreatedDate { get; set; }

        public BookingSubjectType BookingType { get; set; }
        public int TotalBookingSubjects { get; set; }


        public string MainDescription { get; set; }
        public string SubDescription { get; set; }
        public int NoOfBookingSubjectsLeft { get; set; }
        public ICollection<CommentToReturnDto> Comments { get; set; }
        public ICollection<string> Features { get; set; }
        public ICollection<GalleryPictureDto> GalleryPictures { get; set; }
    }
}