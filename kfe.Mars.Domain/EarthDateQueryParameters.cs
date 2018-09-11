using System;
using System.Collections.Generic;
using System.Text;

namespace kfe.Mars.Domain
{
    public class EarthDateQueryParameters
    {
        public DateTime EarthDate { get; set; }
        public string Camera { get; set; }
        public int Page { get; set; }
        public string Api_key { get; set; } = "DEMO_KEY";
    }
}
