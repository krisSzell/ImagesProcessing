using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImagesProcessing.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string Checksum { get; set; }
    }
}