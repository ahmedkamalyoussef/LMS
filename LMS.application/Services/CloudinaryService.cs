using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LMS.Application.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var cloudName = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:ApiKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];

            _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Failed to upload image: {uploadResult.Error?.Message}");
            }

            return uploadResult.SecureUrl.AbsoluteUri;
        }

        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            using var stream = file.OpenReadStream();
            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(file.FileName, stream)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Failed to upload video");
            }

            return uploadResult.SecureUrl.AbsoluteUri;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            using var stream = file.OpenReadStream();
            var uploadParams = new RawUploadParams()
            {
                File = new FileDescription(file.FileName, stream)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Failed to upload file");
            }

            return uploadResult.SecureUrl.AbsoluteUri;
        }

        public async Task DeleteImageAsync(string url)
        {
            await DeleteFileAsync(url);
        }

        public async Task DeleteVideoAsync(string url)
        {
            await DeleteFileAsync(url, ResourceType.Video);
        }

        public async Task DeleteRawFileAsync(string url)
        {
            await DeleteFileAsync(url, ResourceType.Raw);
        }

        private async Task DeleteFileAsync(string url, ResourceType resourceType = ResourceType.Image)
        {
            try
            {
                var publicId = GetPublicIdFromUrl(url);
                var deletionParams = new DeletionParams(publicId) { ResourceType = resourceType };
                var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

                if (deletionResult.Result != "ok")
                {
                    throw new Exception($"Failed to delete file: {deletionResult.Error?.Message}");
                }
            }
            catch
            {
                throw new Exception("Failed to delete the file");
            }
        }

        private string GetPublicIdFromUrl(string url)
        {
            var uri = new Uri(url);
            var path = uri.AbsolutePath;
            return path[(path.LastIndexOf('/') + 1)..].Split('.')[0];
        }
    }
}
