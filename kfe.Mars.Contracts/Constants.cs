using System;
using System.Collections.Generic;
using System.Text;

namespace kfe.Mars.Contracts
{
    public class Constants
    {
        public enum Rovers
        {
            Curiosity = 1,
            Opportunity = 2,
            Spirit = 4,
            All = 7
        }

        public enum Cameras
        {
            FHAZ = 1,
            RHAZ = 2,
            MAST = 3,
            CHEMCAM = 4,
            MAHLI = 5,
            MARDI = 6,
            NAVCAM = 7,
            PANCAM = 8,
            MINITES = 9
        }


    }
}
