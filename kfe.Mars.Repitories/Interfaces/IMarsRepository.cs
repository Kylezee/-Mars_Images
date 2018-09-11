using kfe.Mars.Domain;
using kfe.Mars.Domain.manifest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace kfe.Mars.Repositories.Interfaces
{
    public interface IMarsRepository
    {
        Task<PhotoManifest> GetPhotoManifestsByRover(string rover);
        Task<Domain.Photos.Photos> GetByEarthDate(string rover, DateTime earthDate, string cameraName, int page);
        Task<Domain.Photos.Photos> GetBySolDate(string rover, int solDate, string camera,  int page);


    }
}
