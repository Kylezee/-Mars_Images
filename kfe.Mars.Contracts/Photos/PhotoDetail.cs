﻿using System;
using System.Collections.Generic;
using System.Text;

namespace kfe.Mars.Contracts.Photos
{
public class PhotoDetail
    {
        public int id { get; set; }
        public int sol { get; set; }
        public Camera camera { get; set; }
        public string img_src { get; set; }
        public string earth_date { get; set; }
        public Rover rover { get; set; }
    }
}
