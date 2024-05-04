
using ClinicGraduationProject.web.Extensions;
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

        public async Task<(bool isUploaded, string? errorMessage)> UploadAttachmentAsync(List<IFormFile> attachedFile, string entityName, string userName)
        {
            if(attachedFile.Count > 0)
            {
                foreach (var file in attachedFile)
                {
                    var extension = Path.GetExtension(file.FileName);

                    if (!_allowedExtensions.Contains(extension))
                        return (isUploaded: false, errorMessage: Errors.NotAllowedExtension);

                    if (file.Length > _maxAllowedSize)
                        return (isUploaded: false, errorMessage: Errors.MaxSize);

                    var directoryPath = $"{_webHostEnvironment.WebRootPath}/attachments/{userName}";

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        stream.Dispose();
                    }
                }

            }
            else
            {
                return (isUploaded: false, errorMessage: null);
            }

            return (isUploaded: true, errorMessage: null);
        }
        
    }
}
