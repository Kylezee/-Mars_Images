using System;
using System.Collections.Generic;
using System.Text;

namespace kfe.Mars.Contracts.Photos
{
    public class Camera
    {
        public int id { get; set; }
        public string name { get; set; }
        public int rover_id { get; set; }
        public string full_name { get; set; }
    }
}
