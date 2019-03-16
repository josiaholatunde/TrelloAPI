using System;
using System.Collections.Generic;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.DTO
{
    public class BookingSubjectToReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double AvgRating { 
            get {
                double sum = 0;
                foreach (var item in this.Comments)
                {
                    sum += item.Rating;
                }
                return (sum/this.Comments.Count);
            } 
        }
        public int NoOfVoters { get; set; }
        public int noOfRecommendations { get; set; }
        public DateTime CreatedDate { get; set; }

        public BookingSubjectType BookingType { get; set; }

        public string MainDescription { get; set; }
        public string SubDescription { get; set; }
        public int NoOfBookingSubjectsLeft { get; set; }
        public ICollection<CommentToReturnDto> Comments { get; set; }
        public ICollection<FeatureDto> Features { get; set; }
        public ICollection<GalleryPictureDto> GalleryPictures { get; set; }
    }
}