namespace BlogAPI.BLL.Services.FileStorage
{
    public interface IFileStorage
    {
        Task<string> AddFileAsync(IFormFile file);
        void DeleteFileAsync(string fileName);
    }
}
