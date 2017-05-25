using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImagesProcessing.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            :base("ImagesProcessingContext")
        {

        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Algorithm> Algorithms { get; set; }

    }
}