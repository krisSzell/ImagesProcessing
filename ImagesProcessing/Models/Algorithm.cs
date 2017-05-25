using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImagesProcessing.Models
{
    public class Algorithm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}