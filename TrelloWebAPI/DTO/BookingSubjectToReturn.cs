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
        public string MainPhotoUrl { get; set; }
        public string BookingSubjectResultString { 
            get {
                var resString = $"{Name} {City},{Country}";
                return resString;
            } 
        }

        public double AvgRating { 
            get {
               double sum = 0;
               if (this.Comments.Count > 0)
               {
                    foreach (var item in this.Comments)
                    {
                        sum += item.Rating;
                    }
                    return (sum/this.Comments.Count);
               } 
               return 0;
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