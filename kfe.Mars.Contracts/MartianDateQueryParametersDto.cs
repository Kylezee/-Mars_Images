using System;
using System.Collections.Generic;
using System.Text;

namespace kfe.Mars.Contracts
{
    public class MartianDateQueryParametersDto
    {
        public int Sol { get; set; }
        public string Camera { get; set; }
        public int Page { get; set; }
    }
}
