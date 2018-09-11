using System;
using System.Collections.Generic;
using System.Text;

namespace kfe.Mars.Contracts.PhotoManifest
{
    public class Photo
    {
        public int sol { get; set; }
        public string earth_date { get; set; }
        public int total_photos { get; set; }
        public List<string> cameras { get; set; }
    }
}
