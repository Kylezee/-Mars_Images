using kfe.Mars.Domain.manifest;
using kfe.Mars.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;



namespace kfe.Mars.Repositories
{
    public class MarsRepository: IMarsRepository
    {

        private readonly ILogger _logger;

        private readonly string _baseURI = "https://api.nasa.gov/mars-photos/api/v1/";
        private readonly HttpClient _httpClient;

        public MarsRepository(
            ILogger<MarsRepository> logger)
        {

            _logger = logger;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress= new Uri(_baseURI);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));
        }


        public async Task<Domain.Photos.Photos> GetByEarthDate(string rover, DateTime earthDate, string cameraName,  int page)
        {

            _logger.LogInformation($"Start {nameof(MarsRepository)}:{nameof(GetByEarthDate)}");

            var formattedDate = earthDate.ToString("yyyy-M-d");

            var uri = $"rovers/{rover}/photos?earth_date={formattedDate}&api_key=DEMO_KEY";
            Domain.Photos.Photos photos = null;

            try
            {
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    photos = JsonConvert.DeserializeObject<Domain.Photos.Photos>(content);
                    if (photos.photos.Count > 0)
                    {
                        await DownloadImages(rover, photos);
                    }
                }
            }

            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(MarsRepository)}:{nameof(GetByEarthDate)} {exception}");
            }

            _logger.LogInformation($"Exit {nameof(MarsRepository)}:{nameof(GetByEarthDate)}");



            return photos;
        }

        public async Task<Domain.Photos.Photos> GetBySolDate(string rover, int solDate, string camera,  int page)
        {

            _logger.LogInformation($"Start {nameof(MarsRepository)}:{nameof(GetBySolDate)}");

            var uri = $"rovers/{rover}/photos?sol={solDate}&api_key=DEMO_KEY";
            Domain.Photos.Photos photos = null;

            try
            {
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    photos = JsonConvert.DeserializeObject<Domain.Photos.Photos>(content);
                    await DownloadImages(rover, photos);
                }

            }

            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(MarsRepository)}:{nameof(GetBySolDate)} {exception}");
            }

            _logger.LogInformation($"Exit {nameof(MarsRepository)}:{nameof(GetBySolDate)}");


            return photos;
        }

        public async Task<PhotoManifest> GetPhotoManifestsByRover(string rover)
        {

            _logger.LogInformation($"Start {nameof(MarsRepository)}:{nameof(GetPhotoManifestsByRover)}");
            PhotoManifest manifest = null;

            var uri = $"manifests/{rover}?api_key=DEMO_KEY";

            try
            {
                var response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    manifest = JsonConvert.DeserializeObject<PhotoManifest>(content);
                }
            }

            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(MarsRepository)}:{nameof(GetPhotoManifestsByRover)} {exception}");
            }

            _logger.LogInformation($"Exit {nameof(MarsRepository)}:{nameof(GetPhotoManifestsByRover)}");


            return manifest;

        }

        public async Task<bool> DownloadImages(string rover, Domain.Photos.Photos photos)
        {

            _logger.LogInformation($"Start {nameof(MarsRepository)}:{nameof(DownloadImages)}");

            try
            {

                string path = $"images/{rover}/{photos.photos[0].earth_date}";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (Domain.Photos.PhotoDetail detail in photos.photos)
                {

                    var sourceUri = new Uri(detail.img_src);
                    var sourceFilename = Path.GetFileName(sourceUri.LocalPath);

                    var response = await _httpClient.GetAsync(detail.img_src);

                    if (response.IsSuccessStatusCode)
                    {
                        var image =await response.Content.ReadAsByteArrayAsync();
                        File.WriteAllBytes($"{path}/{sourceFilename}.jpg", image);
                    }
                }
            }

            catch (Exception exception)
            {
                _logger.LogError($"Exception in {nameof(MarsRepository)}:{nameof(DownloadImages)} {exception}");
            }

            _logger.LogInformation($"Exit {nameof(MarsRepository)}:{nameof(DownloadImages)}");




            return true;
        }

    }
}
