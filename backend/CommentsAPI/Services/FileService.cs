using CommentsAPI.Interfaces;

namespace CommentsAPI.Services
{
    public class FileService : IFileService
    {
        private readonly string _baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            if (!Directory.Exists(_baseDirectory))
            {
                Directory.CreateDirectory(_baseDirectory);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_baseDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
