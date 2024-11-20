namespace BlogAPI.BLL.Services.FileStorage
{
    public class FileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _environment;

        public FileStorage(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> AddFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("Файл не завантажено.");
            }

            var uploadPath = Path.Combine(_environment.ContentRootPath, "UploadedFiles");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileExtension = Path.GetExtension(file.FileName);
            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return newFileName;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new Exception("Назва файлу не вказана.");
            }

            var uploadPath = Path.Combine(_environment.ContentRootPath, "UploadedFiles");
            var filePath = Path.Combine(uploadPath, fileName);

            if (!File.Exists(filePath))
            {
                throw new Exception("Файл не знайдено.");
            }

            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка при видаленні файлу: {ex.Message}");
            }
        }
    }

}
