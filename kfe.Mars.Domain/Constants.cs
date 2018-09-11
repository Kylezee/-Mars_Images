using System;
using System.Collections.Generic;

namespace kfe.Mars.Domain
{
    public class Constants
    {
        [Flags]
        public enum Rovers
        {
            Curiosity = 1,
            Opportunity = 2,
            Spirit = 4
        }
        public enum Cameras
        {
            FHAZ,
            RHAZ,
            MAST,
            CHEMCAM,
            MAHLI,
            MARDI,
            NAVCAM,
            PANCAM,
            MINITES
        }

    }
}
