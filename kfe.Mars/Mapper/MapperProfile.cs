using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using kfe.Mars.Contracts;
using kfe.Mars.Contracts.PhotoManifest;
using kfe.Mars.Domain;
using kfe.Mars.Domain.manifest;

using kfe.Mars.Contracts.Photos;

namespace kfe.Mars.Mapper
{

    /// <summary>
    /// Auto Mapper between Dto and domain
    /// </summary>
    public class MapperProfile:Profile
    {

        /// <summary>
        /// constructor 
        /// </summary>
        public MapperProfile()
        {
            CreateMap<EarthDateQueryParametersDto, EarthDateQueryParameters>().ReverseMap();

            CreateMap<MartianDateQueryParametersDto, MartianDateQueryParameters>().ReverseMap();


            CreateMap<Contracts.PhotoManifest.Manifest, Domain.manifest.Manifest>().ReverseMap();
            CreateMap<Contracts.PhotoManifest.Photo, Domain.manifest.Photo>().ReverseMap();
            CreateMap<PhotoManifest, PhotoManifestDto>().ReverseMap();

            CreateMap<Contracts.Photos.Rover, Domain.Photos.Rover>().ReverseMap();
            CreateMap<Contracts.Photos.Camera, Domain.Photos.Camera>().ReverseMap();
            CreateMap<Contracts.Photos.PhotoDetail, Domain.Photos.PhotoDetail>().ReverseMap();
            CreateMap<PhotosDto, Domain.Photos.Photos>().ReverseMap();


        }
    }
}
