using ImagesProcessing.Models;
using ImagesProcessing.Persistence;
using ImagesProcessing.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace ImagesProcessing.Controllers
{
    public class PhotosController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public PhotosController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        public IHttpActionResult Get()
        {
            var photos = _unitOfWork.Photos.GetAllPhotos();

            return Ok(photos);
        }

        public IHttpActionResult Get(int id)
        {
            var photo = _unitOfWork.Photos.GetPhotoById(id);
            if (photo == null)
            {
                return NotFound();
            }

            return Ok(photo);
        }

        public IHttpActionResult Post(Photo photo)
        {
            if (_unitOfWork.Photos.GetPhotoChecksum(photo) != null)
            {
                var message = $"Photo already exists in database.";

                return Content(HttpStatusCode.Conflict, message);
            }

            _unitOfWork.Photos.AddPhoto(photo);
            _unitOfWork.Complete();

            return Created(Url.Link("DefaultApi", new { controller = "Photos", id = photo.Id }), photo);
        }

        [Route("api/photos/{id}")]
        [HttpPut]
        public IHttpActionResult UpdatePhoto(int id, Photo photo)
        {
            var photoInContext = _unitOfWork.Photos.GetPhotoById(id);
            if (photoInContext.Checksum == photo.Checksum)
            {
                var message = $"Photo you tried to update seems exactly the same as original.";

                return Content(HttpStatusCode.BadRequest, message);
            }

            photoInContext.Id = id;
            photoInContext.Checksum = photo.Checksum;
            photoInContext.Data = photo.Data;
            _unitOfWork.Complete();

            return Ok(photo);
        }

        [Route("api/photos/test")]
        [HttpPost]
        public IHttpActionResult Test(byte[] array)
        {
            var photo = new Photo()
            {
                Data = "test",
                Checksum = "checksum",
                PhData = array
            };
            _context.Photos.Add(photo);
            _context.SaveChanges();

            return Created(HttpContext.Current.Server.MapPath("~/") + "api/photos/test", photo);
        }

        [Route("api/photos/test")]
        [HttpGet]
        public IHttpActionResult Test()
        {
            var photo = _context.Photos.Single(p => p.Id == 7);

            return Ok(photo.PhData);
        }
    }
}
