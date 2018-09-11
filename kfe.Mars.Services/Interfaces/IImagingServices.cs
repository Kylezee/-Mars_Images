using kfe.Mars.Domain.manifest;
using System;
using System.Threading.Tasks;

namespace kfe.Mars.Services.Interfaces
{
    public interface IImagingServices
    {
        Task<PhotoManifest> GetPhotoManifestByRover(string rover);
        Task<Domain.Photos.Photos> GetImageListByEarthDate(string roverName, string cameraName, DateTime earthDate, int page);
        Task<Domain.Photos.Photos> GetImageListByMartianSol(string rover, string camera, int solDate, int page);

    }
}
