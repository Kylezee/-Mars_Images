using kfe.Mars.Domain.manifest;
using kfe.Mars.Repositories.Interfaces;
using kfe.Mars.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace kfe.Mars.Services
{
    public class ImagingServices : IImagingServices
    {
        private readonly ILogger _logger;
        private readonly IMarsRepository _marsRepository;

        public ImagingServices(
            ILogger<ImagingServices> logger,
            IMarsRepository marsRepository
            )
        {
            _logger = logger;
            _marsRepository = marsRepository;
        }


        public async Task<PhotoManifest> GetPhotoManifestByRover(string rover)
        {

            _logger.LogInformation($"Start {nameof(ImagingServices)}:{nameof(GetPhotoManifestByRover)}");

            if (!Enum.IsDefined(typeof(Domain.Constants.Rovers), rover))
            {
                return null;
            }


            PhotoManifest manifest = null;

            try
            {
                manifest = await _marsRepository.GetPhotoManifestsByRover(rover);
            }

            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(ImagingServices)}:{nameof(GetPhotoManifestByRover)} {exception}");
            }

            _logger.LogInformation($"Exit {nameof(ImagingServices)}:{nameof(GetPhotoManifestByRover)}");
            

            return manifest;
        }

        public async Task<Domain.Photos.Photos> GetImageListByEarthDate(string roverName,  string cameraName, DateTime earthDate, int page)
        {
            // camera name and page are not implemented yet

            if (!Enum.IsDefined(typeof(Domain.Constants.Rovers), roverName))
            {
                return null;
            }

            _logger.LogInformation($"Start {nameof(ImagingServices)}:{nameof(GetImageListByEarthDate)}");

            Domain.Photos.Photos photos = null;

            try
            {
                if (roverName.Contains("All"))
                {
                    foreach( Domain.Constants.Rovers roverEnum in Enum.GetValues(typeof(Domain.Constants.Rovers)))
                    {
                       photos = await _marsRepository.GetByEarthDate(roverEnum.ToString(), earthDate, string.Empty, 0);
                    }
                }
                else
                {
                    photos = await _marsRepository.GetByEarthDate(roverName, earthDate, string.Empty, 0);
                }
            }

            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(ImagingServices)}:{nameof(GetImageListByEarthDate)} {exception}");
            }

            _logger.LogInformation($"Exit {nameof(ImagingServices)}:{nameof(GetImageListByEarthDate)}");

            return photos;
        }
        
        public async Task<Domain.Photos.Photos> GetImageListByMartianSol(string rover, string camera, int solDate, int page)
        {
            _logger.LogInformation($"Start {nameof(ImagingServices)}:{nameof(GetImageListByMartianSol)}");

            Domain.Photos.Photos photos = null;

            try
            {

                if (rover.Contains("All"))
                {
                    foreach (Domain.Constants.Rovers roverEnum in Enum.GetValues(typeof(Domain.Constants.Rovers)))
                    {
                        photos = await _marsRepository.GetBySolDate(roverEnum.ToString(), solDate, string.Empty, 0);
                    }
                }
                else
                {
                    photos = await _marsRepository.GetBySolDate(rover, solDate, string.Empty, 0);
                }

            }

            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(ImagingServices)}:{nameof(GetImageListByMartianSol)} {exception}");
            }

            _logger.LogInformation($"Exit {nameof(ImagingServices)}:{nameof(GetImageListByMartianSol)}");

            return photos;

        }

    }
}
