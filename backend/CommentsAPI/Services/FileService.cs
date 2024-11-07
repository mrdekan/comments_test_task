using CommentsAPI.Interfaces;
using ImageMagick;

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
            if (new string[] { "jpg", "png", "gif" }.Contains(Path.GetExtension(file.FileName).ToLower().Replace(".", "")))
            {
                ProcessImage(240, 320, fileName);
            }
            return fileName;
        }
        public void ProcessImage(int maxWidth, int maxHeight, string filename)
        {
            filename = Path.Combine(_baseDirectory, filename);
            MagickImage img = new(filename);

            if (img.Width > maxWidth || img.Height > maxHeight)
            {
                decimal ratio = img.Width / img.Height;
                decimal newRatio = maxWidth / maxHeight;
                if (ratio > 1 && newRatio < 1 || ratio < 1 && newRatio > 1)
                    (maxWidth, maxHeight) = (maxHeight, maxWidth);
                decimal widthRatio = (decimal)maxWidth / img.Width;
                decimal heightRatio = (decimal)maxHeight / img.Height;

                decimal scaleRatio = Math.Min(widthRatio, heightRatio);

                int newWidth = (int)Math.Round(img.Width * scaleRatio);
                int newHeight = (int)Math.Round(img.Height * scaleRatio);

                img.Resize((uint)newWidth, (uint)newHeight);
            }

            img.Write(filename);
        }

    }
}
