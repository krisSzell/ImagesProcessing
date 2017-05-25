using ImagesProcessing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImagesProcessing.Repositories
{
    public class PhotoRepository
    {
        private readonly ApplicationDbContext _context;

        public PhotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Photo> GetAllPhotos()
        {
            return _context.Photos.ToList();
        }

        public Photo GetPhotoById(int id)
        {
            return _context.Photos.SingleOrDefault(p => p.Id == id);
        }

        public string GetPhotoChecksum(Photo photo)
        {
            var photoToGet = _context.Photos.SingleOrDefault(p => p.Checksum == photo.Checksum);
            if (photoToGet == null)
                return null;

            return photoToGet.Checksum;
        }

        public void AddPhoto(Photo photo)
        {
            _context.Photos.Add(photo);
        }
    }
}