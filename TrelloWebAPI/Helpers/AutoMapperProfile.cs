using System.Linq;
using AutoMapper;
using TrelloWebAPI.DTO;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            //Domain to APIResource
            CreateMap<BookingSubject, BookingSubjectToReturn>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Take(2)))
            .ForMember(dest => dest.GalleryPictures, opt => opt.MapFrom(src => src.GalleryPictures.Take(3)))
            .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src => src.GalleryPictures.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(dest => dest.NoOfVoters, opt => opt.MapFrom(src => src.Comments.Count()))
            .ForMember(m => m.noOfRecommendations, opt => opt.MapFrom(src => src.Comments.Where(m => m.IsRecommended).Count()));

            CreateMap<BookingSubject, BookingSubjectForEditDetail>()
            .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features.Select(ft => ft.Title)));

            CreateMap<Comment, CommentToReturnDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FirstName + " " +src.User.LastName))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.User.PhotoUrl));

            CreateMap<GalleryPicture, PhotoToReturnDto>();

            CreateMap<Photo, PhotoToReturnDto>();

            CreateMap<Message, MessageToReturnDto>()
            .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src => src.Recipient.Photo.Url))
            .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => src.Sender.Photo.Url))
            .ForMember(dest => dest.SenderKnownAs, opt => opt.MapFrom(src => src.Sender.UserName))
            .ForMember(dest => dest.RecipientKnownAs, opt => opt.MapFrom(src => src.Recipient.UserName));

            //API Reource to API Domain
            CreateMap<BookingToCreateDto, BookingSubject>()
            .ForMember(dest => dest.MainDescription, opt => opt.MapFrom(src => src.Description.MainDescription))
            .ForMember(dest => dest.SubDescription, opt => opt.MapFrom(src => src.Description.SubDescription))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location.City))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Location.Country))
            .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features.Select(ft => new Feature { Title = ft })))
            .ReverseMap();

            CreateMap<UserForRegisterationDto, User>();

            CreateMap<User, UserToReturnDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photo.Url));
            CreateMap<CommentDto, Comment>();
            CreateMap<PhotoToCreateDto, GalleryPicture>();
            CreateMap<PhotoToCreateDto, Photo>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
    

        }
    }
}