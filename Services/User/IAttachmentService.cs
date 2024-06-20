namespace Speedy.Services.User
{
    public interface IAttachmentService
    {
        public Task<(bool isUploaded, string? errorMessage, List<string>? AttachmentUrls)> UploadAttachmentAsync(List<IFormFile> attachedFile, string entityName, string userName);
        public Task<(bool isUploaded, string? errorMessage, string? AttachmentUrl)> UploadImageAsync(IFormFile attachedFile, string entityName, string userName);
    }
}

