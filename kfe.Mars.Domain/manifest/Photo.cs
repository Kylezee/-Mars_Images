using System.Collections.Generic;

namespace kfe.Mars.Domain.manifest
{
    public class Photo
    {
        public int sol { get; set; }
        public string earth_date { get; set; }
        public int total_photos { get; set; }
        public List<string> cameras { get; set; }
    }
}
