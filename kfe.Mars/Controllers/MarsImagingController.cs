using AutoMapper;
using kfe.Mars.Contracts;
using kfe.Mars.Domain.manifest;
using kfe.Mars.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using kfe.Mars.Contracts.PhotoManifest;

namespace kfe.Mars.Controllers
{
    /// <summary>
    /// Mars Imaging Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/marsImaging")]
    [Produces("application/json")]
    public class MarsImagingController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IImagingServices _imagingServices;

        /// <summary>
        /// Mars Imaging Controller Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="imagingServices"></param>
        public MarsImagingController(
            ILogger<MarsImagingController> logger,
            IMapper mapper,
            IImagingServices imagingServices
            )
        {
            _logger = logger;
            _mapper = mapper;
            _imagingServices = imagingServices;
        }

        /// <summary>
        /// Gets Mars Images by Earth date
        /// </summary>
        /// <param name="rover"></param>
        /// <param name="earthDate"></param>
        /// <returns></returns>
        [HttpGet("earthDate")]
        public async Task<IActionResult> GetImageListByEarthDate(Constants.Rovers rover, DateTime earthDate)
        {
            _logger.LogInformation($"Start {nameof(MarsImagingController)}:{nameof(GetImageListByEarthDate)}");

            Contracts.Photos.PhotosDto photos = null;

            try
            {
                var result = await _imagingServices.GetImageListByEarthDate(rover.ToString(), string.Empty, earthDate, 0);
                photos = _mapper.Map<Contracts.Photos.PhotosDto>(result);
            }

            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(MarsImagingController)}:{nameof(GetImageListByEarthDate)} {exception}");
            }

            _logger.LogInformation($"Exit {nameof(MarsImagingController)}:{nameof(GetImageListByEarthDate)}");

            return Ok(photos);
        }

        /// <summary>
        /// Get Image List by Sol Date
        /// </summary>
        ///<param name="rover"></param>
        ///<param name="solDate"></param>
        /// <returns></returns>
        [HttpGet("solDate")]
        public async Task<IActionResult> GetImageListBySolDate(Constants.Rovers rover, int solDate)

        {
            _logger.LogInformation($"Start {nameof(MarsImagingController)}:{nameof(GetImageListBySolDate)}");

            Contracts.Photos.PhotosDto photos = null;

            try
            {
                var result = await _imagingServices.GetImageListByMartianSol(rover.ToString(), string.Empty, solDate, 0);
                photos = _mapper.Map<Contracts.Photos.PhotosDto>(result);
            }

            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(MarsImagingController)}:{nameof(GetImageListBySolDate)} {exception}");
            }

            _logger.LogInformation($"Exit {nameof(MarsImagingController)}:{nameof(GetImageListBySolDate)}");

            return Ok(photos);

        }

        /// <summary>
        /// Get a manifest list of photos by Rover
        /// </summary>
        /// <param name="rover"></param>
        /// <returns></returns>
        [HttpGet("rovers")]
        public async Task<IActionResult> GetPhotoManifestByRover(Contracts.Constants.Rovers rover)
        {
            _logger.LogInformation($"Start {nameof(MarsImagingController)}:{nameof(GetPhotoManifestByRover)}");
            PhotoManifestDto dto = null;
            try
            {
                var photoManifest =await  _imagingServices.GetPhotoManifestByRover(rover.ToString());
                dto = _mapper.Map<PhotoManifestDto>(photoManifest);
            }

            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(MarsImagingController)}:{nameof(GetPhotoManifestByRover)} {exception}");
            }

            _logger.LogInformation($"Exit {nameof(MarsImagingController)}:{nameof(GetPhotoManifestByRover)}");


            return Ok(dto);
        }
    }
}
