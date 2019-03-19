using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TrelloWebAPI.Data;
using TrelloWebAPI.DTO;
using TrelloWebAPI.Helpers;
using TrelloWebAPI.Models;

namespace TrelloWebAPI.Controllers
{
   [Route("api")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private readonly IHostingEnvironment _hosting;
        private Cloudinary _cloudinary;

        public PhotosController(IRepository repository, IAuthRepository authRepository,IMapper mapper, 
        IOptions<CloudinarySettings> cloudinaryConfig, IHostingEnvironment hosting)
        {
            _repository = repository;
            _authRepository = authRepository;
            _mapper = mapper;
            _hosting = hosting;

            Account account = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
            
        }

        [HttpGet("bookings/{bookingId}/photos")]
        public async Task<IActionResult> GetGalleryPhotosForBooking(int bookingId)
        {
            var photoFromRepos = await _repository.GetGalleryPhotos(bookingId);
            var photoToReturn = _mapper.Map<IEnumerable<PhotoToReturnDto>>(photoFromRepos);
            return Ok(photoToReturn);
        }

        // GET api/values
        [HttpGet("bookings/{bookingId}/photos/{id}", Name="GetPhoto")]
        public async Task<IActionResult> GetGalleryPhoto(int bookingId, int photoId)
        {
            var photoFromRepo = await _repository.GetGalleryPhoto(bookingId, photoId);
            return Ok(photoFromRepo);
        }

        [HttpGet("users/{userId}/photos/{id}", Name="GetPhotoForUser")]
        public async Task<IActionResult> GetPhotoForUser(int userId, int id)
        {
            var photoFromRepo = await _repository.GetUserPhoto(userId, id);
            return Ok(photoFromRepo);
        }

        [Authorize]
        [HttpPost("bookings/{bookingId}/photos/{userId}")]
        public async Task<IActionResult> CreateGalleryPhoto(int bookingId, int userId, [FromForm] PhotoToCreateDto photoToCreateDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _repository.GetUser(userId);
            if (userFromRepo == null || userFromRepo.UserRole != UserRole.Admin)
                return Unauthorized();
            var bookingFromRepo = await _repository.GetBooking(bookingId);
            if (bookingFromRepo == null)
                return BadRequest("Booking does not exist");
            var file = photoToCreateDto.File;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var imageUploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Width(600).Height(600).Crop("fill")
                    };
                    uploadResult = _cloudinary.Upload(imageUploadParams);
                }
            }
            photoToCreateDto.Url = uploadResult.Uri.ToString();
            photoToCreateDto.PublicId = uploadResult.PublicId;
            if (bookingFromRepo.GalleryPictures.Count == 0)
                photoToCreateDto.IsMain = true;
       
            var fileToUpload =  _mapper.Map<GalleryPicture>(photoToCreateDto);
            bookingFromRepo.GalleryPictures.Add(fileToUpload);
            if (await _repository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoToReturnDto>(fileToUpload);
                return CreatedAtRoute("GetPhoto", new { id = photoToReturn.Id, bookingId = bookingId}, photoToReturn);
            }

            return BadRequest("An Error occurred while uploading the photo");
        }
        [Authorize]
        [HttpDelete("bookings/{bookingId}/photos/{photoId}/{userId}")]
        public async Task<IActionResult> DeleteGalleryPhoto(int bookingId, int userId, int photoId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = await _repository.GetUser(userId);
            if (userFromRepo == null || userFromRepo.UserRole != UserRole.Admin)
                return Unauthorized();
            var bookingFromRepo = await _repository.GetBooking(bookingId);
            if (bookingFromRepo == null)
                return BadRequest("Booking does not exist");
            var photoFromRepo = await _repository.GetGalleryPhoto(bookingId, photoId);
            if(photoFromRepo == null)
                return BadRequest("Photo does not exist");
            if (photoFromRepo.IsMain)
                return BadRequest("You can not delete the main photo");
            if (photoFromRepo.PublicId != null)
            {
                var deletionParams = new DeletionParams(photoFromRepo.PublicId);
                var result = _cloudinary.Destroy(deletionParams);
                if (result.Result == "ok")
                    _repository.Delete<GalleryPicture>(photoFromRepo);  
            }
            if (photoFromRepo.PublicId == null)
                _repository.Delete<GalleryPicture>(photoFromRepo); 
            if (await _repository.SaveAll()) 
                return NoContent();
  
            return BadRequest("Error occurred while deleting gallery photo");
        }
        [Authorize]
        [HttpPost("users/{userId}/photos")]
        public async Task<IActionResult> CreateUserPhoto(int userId, [FromForm] PhotoToCreateDto photoToCreateDto)
        {
            var userFromRepo = await _repository.GetUser(userId);
            if (userFromRepo == null)
                return BadRequest("User does not exist");
            var file = photoToCreateDto.File;
            photoToCreateDto.UserId = userId;
            var photoUploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var imageUploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    photoUploadResult = _cloudinary.Upload(imageUploadParams);
                }
            }
            photoToCreateDto.Url = photoUploadResult.Uri.ToString();
            photoToCreateDto.PublicId = photoUploadResult.PublicId;
       
            var photoForUpload =  _mapper.Map<Photo>(photoToCreateDto);
            _repository.Add(photoForUpload);
            if (await _repository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoToReturnDto>(photoForUpload);
                return CreatedAtRoute("GetPhotoForUser", new { id = photoToReturn.Id, userId = userFromRepo.Id}, photoToReturn);
            }
            return BadRequest("An Error occurred while uploading the photo");
        }

       /*  [HttpPost]
        public async Task<IActionResult> CreateUserPhoto(int bookingId, [FromForm] PhotoToCreateDto photoToCreateDto)
        {
            var bookingFromRepo = await _repository.GetBooking(bookingId);
            if (bookingFromRepo == null)
                return BadRequest("Booking does not exist");
            var uploadFolder = Path.Combine(_hosting.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);
            //Change File Name
            var fileName = Guid.NewGuid() + Path.GetExtension(photoToCreateDto.File.FileName);
            var fileUploadPath = Path.Combine(uploadFolder, fileName);
            using (var stream = new FileStream(fileUploadPath, FileMode.Create))
            {
                await photoToCreateDto.File.CopyToAsync(stream);
            }
            photoToCreateDto.Url = fileUploadPath;
            var fileToUpload =  _mapper.Map<GalleryPicture>(photoToCreateDto);
            bookingFromRepo.GalleryPictures.Add(fileToUpload);
            if (await _repository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoToReturnDto>(fileToUpload);
                return CreatedAtRoute("GetPhotos", new { id = photoToReturn.Id}, photoToReturn);
            }

            return BadRequest("An Error occurred while uploading the photo");
        } */
 
    
        
    }
}