using ImagesProcessing.Models;
using ImagesProcessing.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImagesProcessing.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public PhotoRepository Photos { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Photos = new PhotoRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}