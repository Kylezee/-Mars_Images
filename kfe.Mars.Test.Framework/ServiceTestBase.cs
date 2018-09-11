using System;
using kfe.Mars.Services;
using Moq;
using Microsoft.Extensions.Logging;

namespace kfe.Mars.Test.Framework
{
    public class ServiceTestBase
    {
        protected readonly Mock<ILogger<ImagingServices>> _loggerImaging;

        public ServiceTestBase()
        {
            _loggerImaging = new Mock<ILogger<ImagingServices>>();
        }

    }

    

}
