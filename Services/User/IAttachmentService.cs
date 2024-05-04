namespace Speedy.Services.User
{
    public interface IAttachmentService
    {
        public Task<(bool isUploaded, string? errorMessage)> UploadAttachmentAsync(List<IFormFile> attachedFile, string entityName, string userName);
    }
}
