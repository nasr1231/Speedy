
using Microsoft.AspNetCore.Hosting;
using NuGet.Packaging.Signing;
using Speedy.Core.Consts;
using System.IO;
using System.Net.Mail;

namespace Speedy.Services.User
{
    public class AttachmentService(IWebHostEnvironment webHostEnvironment) : IAttachmentService
    {
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly List<string> _allowedExtensions = [".jpg", ".jpeg", ".png", ".pdf"];
        private readonly int _maxAllowedSize = 2097152;

        public async Task<(bool isUploaded, string? errorMessage, List<string>? AttachmentUrls)> UploadAttachmentAsync(List<IFormFile> attachedFile, string entityName, string userName)
        {
            if (attachedFile.Count <= 0)
                return (isUploaded: false, errorMessage: null, AttachmentUrls: null);

            List<string> attachmentUrls = []; 

            foreach (var file in attachedFile)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!_allowedExtensions.Contains(extension))
                    return (isUploaded: false, errorMessage: Errors.NotAllowedExtension, null);

                if (file.Length > _maxAllowedSize)
                    return (isUploaded: false, errorMessage: Errors.MaxSize, null);

                var directoryPath = $"{_webHostEnvironment.WebRootPath}/attachments/Orders/{userName}";

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, file.FileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Dispose();

                // Return urls to controller
                attachmentUrls.Add(filePath);
            }


            return (isUploaded: true, errorMessage: null, AttachmentUrls: attachmentUrls);
        }

        public async Task<(bool isUploaded, string? errorMessage, string? AttachmentUrl)> UploadImageAsync(IFormFile attachedFile, string entityName, string userName)
        {
            var extension = Path.GetExtension(attachedFile.FileName);

            if (!_allowedExtensions.Contains(extension))
                return (isUploaded: false, errorMessage: Errors.NotAllowedExtension, null);

            if (attachedFile.Length > _maxAllowedSize)
                return (isUploaded: false, errorMessage: Errors.MaxSize, null);

            var imageName = $"{Guid.NewGuid()}{extension}";

            var path = Path.Combine($"{_webHostEnvironment.WebRootPath}/attachments/Orders/", imageName);

            using var stream = System.IO.File.Create(path);
            attachedFile.CopyTo(stream);

            return (isUploaded: true, errorMessage: null, AttachmentUrl: path);
        }
    }
   
}
