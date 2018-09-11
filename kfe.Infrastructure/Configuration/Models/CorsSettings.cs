using System;
using System.Collections.Generic;
using System.Text;

namespace kfe.Infrastructure.Configuration.Models
{
    public class CorsSettings
    {
        public string CorsProfile { get; set; }
        public List<string> Domains { get; set; }
    }
}
