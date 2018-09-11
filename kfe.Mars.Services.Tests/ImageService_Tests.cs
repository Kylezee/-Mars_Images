using System;
using System.Threading.Tasks;
using Xunit;
using kfe.Mars.Repositories.Interfaces;
using kfe.Mars.Test.Framework;
using Moq;
using kfe.Mars.Domain.manifest;
using kfe.Mars.Domain.Photos;
using System.Collections.Generic;

namespace kfe.Mars.Services.Tests
{
    public class ImageService_Tests: ServiceTestBase
    {
        public ImageService_Tests()
            :base()
        {

        }
        
        #region Get Photo Manifest by Rover

        [Fact]
        public async Task GetPhotoManifestByRover_Good_RoverName()
        {
            var marsRepoMock = new Mock<IMarsRepository>();

            marsRepoMock.Setup(
                rmr => rmr.GetPhotoManifestsByRover(It.IsAny<string>()))
                .Returns(Task.FromResult(PhotoManifestData));

            var service = new ImagingServices(
                _loggerImaging.Object,
                marsRepoMock.Object);

            var response = await service.GetPhotoManifestByRover("Curiosity");


            Assert.Equal(PhotoManifestData.photo_manifest.name, response.photo_manifest.name);
            Assert.Equal(PhotoManifestData.photo_manifest.landing_date, response.photo_manifest.landing_date);
            Assert.Equal(PhotoManifestData.photo_manifest.total_photos, response.photo_manifest.total_photos);
            Assert.Equal(PhotoManifestData.photo_manifest.photos[0].earth_date, response.photo_manifest.photos[0].earth_date);
            Assert.Equal(PhotoManifestData.photo_manifest.photos[0].cameras[0], response.photo_manifest.photos[0].cameras[0]);

            Assert.True(true);
        }

        [Fact]
        public async Task GetPhotoManifestByRover_No_RoverName()
        {
            var marsRepoMock = new Mock<IMarsRepository>();

            marsRepoMock.Setup(
                rmr => rmr.GetPhotoManifestsByRover(It.IsAny<string>()))
                .Returns(Task.FromResult((PhotoManifest)null));

            var service = new ImagingServices(
                _loggerImaging.Object,
                marsRepoMock.Object);

            var response = await service.GetPhotoManifestByRover(string.Empty);

            Assert.Null(response);
        }

        [Fact]
        public async Task GetPhotoManifestByRover_Bad_RoverName()
        {

            var marsRepoMock = new Mock<IMarsRepository>();

            marsRepoMock.Setup(
                rmr => rmr.GetPhotoManifestsByRover(It.IsAny<string>()))
                .Returns(Task.FromResult((PhotoManifest)null));

            var service = new ImagingServices(
                _loggerImaging.Object,
                marsRepoMock.Object);

            var response = await service.GetPhotoManifestByRover("Kyle");

            Assert.Null(response);
        }
        #endregion

        #region Get ImagelistByEarthDate


        [Fact]
        public async Task GetImageListByEarthDate_Good_Parameters()
        {
            var marsRepoMock = new Mock<IMarsRepository>();

            marsRepoMock.Setup(
                rmr => rmr.GetByEarthDate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(RoverPhotos));

            var service = new ImagingServices(
                _loggerImaging.Object,
                marsRepoMock.Object);

            var response = await service.GetImageListByEarthDate("Curiosity", string.Empty, new DateTime(2017, 2,27), 0);

            Assert.Equal(RoverPhotos.photos[0].id, response.photos[0].id);
            Assert.Equal(RoverPhotos.photos[0].camera.id , response.photos[0].camera.id);
            Assert.Equal(RoverPhotos.photos[0].rover.id , response.photos[0].rover.id);
            Assert.Equal(RoverPhotos.photos[0].rover.cameras[0].id, response.photos[0].rover.cameras[0].id);
           
        }

        [Fact]
        public async Task GetImageListByEarthDate_Missing_Rover()
        {


            var marsRepoMock = new Mock<IMarsRepository>();

            marsRepoMock.Setup(
                rmr => rmr.GetByEarthDate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult((Photos)null));

            var service = new ImagingServices(
                _loggerImaging.Object,
                marsRepoMock.Object);

            var response = await service.GetImageListByEarthDate(string.Empty, string.Empty, new DateTime(2017, 2, 27), 0);


            Assert.Null(response);
        }

        

        [Fact]
        public async Task GetImageListByEarthDate_Bad_RoverName()
        {

            var marsRepoMock = new Mock<IMarsRepository>();

            marsRepoMock.Setup(
                rmr => rmr.GetByEarthDate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult((Photos)null));

            var service = new ImagingServices(
                _loggerImaging.Object,
                marsRepoMock.Object);

            var response = await service.GetImageListByEarthDate("Bad Robot", string.Empty, new DateTime(2017, 2, 27), 0);


            Assert.Null(response);
        }

        #endregion

        #region Get Image List by Mars Sol

        [Fact]
        public async Task GetImageListByMartianSol_Good_Parameters()
        {
            var marsRepoMock = new Mock<IMarsRepository>();

            marsRepoMock.Setup(
                rmr => rmr.GetBySolDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(RoverPhotos));

            var service = new ImagingServices(
                _loggerImaging.Object,
                marsRepoMock.Object);

            var response = await service.GetImageListByMartianSol("Curiosity", string.Empty, 123, 0);

            Assert.Equal(RoverPhotos.photos[0].id, response.photos[0].id);
            Assert.Equal(RoverPhotos.photos[0].camera.id, response.photos[0].camera.id);
            Assert.Equal(RoverPhotos.photos[0].rover.id, response.photos[0].rover.id);
            Assert.Equal(RoverPhotos.photos[0].rover.cameras[0].id, response.photos[0].rover.cameras[0].id);

        }

        [Fact]
        public async void GetImageListByMartianSol_Missing_Rover()
        {

            var marsRepoMock = new Mock<IMarsRepository>();

            marsRepoMock.Setup(
                rmr => rmr.GetBySolDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult((Photos)null));

            var service = new ImagingServices(
                _loggerImaging.Object,
                marsRepoMock.Object);

            var response = await service.GetImageListByMartianSol(string.Empty, string.Empty, 123, 0);


            Assert.Null(response);
        }

        [Fact]
        public async Task GetImageListByMartianSol_Bad_RoverName()
        {

            var marsRepoMock = new Mock<IMarsRepository>();

            marsRepoMock.Setup(
                rmr => rmr.GetBySolDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult((Photos)null));

            var service = new ImagingServices(
                _loggerImaging.Object,
                marsRepoMock.Object);

            var response = await service.GetImageListByMartianSol("Bad Robot", string.Empty, 123, 0);


            Assert.Null(response);
        }
        #endregion

        #region Repository Mock Data

        public PhotoManifest PhotoManifestData = new PhotoManifest()
        {
            photo_manifest = new Manifest()
            {
                name = "Curiosity",
                landing_date = "6/21/2018",
                launch_date = "3/21/2018",
                status = "active",
                max_sol = 2000,
                max_date = "12/31/2018",
                total_photos = 123456,
                photos = new List<Photo>()
                {
                    new Photo()
                    {
                        sol = 1622,
                        earth_date = "2/27/2017",
                        total_photos = 36,
                        cameras = new List<string>()
                        {
                            "Front",
                            "side",
                            "reverse"
                        }
                    }
                }
            },

        };

        public Photos RoverPhotos = new Domain.Photos.Photos()
        {
            photos = new List<PhotoDetail>()
            {
                new PhotoDetail()
                {
                    id = 1,
                     camera = new Camera()
                     {
                         id = 1,
                         name = "Front Camera",
                         rover_id = 123,
                         full_name = "Tst Camera",
                     },
                     img_src = "Test.jpg",
                    earth_date = "2/27/2017",
                    rover = new Rover()
                    {
                        id=1,
                        name = "Kyle's Rover",
                        landing_date = "1/1/2017",
                        launch_date = "6/30/2016",
                        status = "Active",
                        max_sol = 1234,
                        max_date = "1/1/2019",
                        total_photos = 123456,
                        cameras = new List<Camera>()
                        {
                            new Camera()
                            {
                                id = 1,
                                rover_id = 1,
                                name = "FSC",
                                full_name ="Front Collision camera"
                            }
                        }
                    }
                }
            }
        };

        #endregion
    }
}
