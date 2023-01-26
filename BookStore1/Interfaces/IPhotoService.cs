using CloudinaryDotNet.Actions;
using Microsoft.Identity.Client;

namespace BookStore1.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicID);
    }
}
