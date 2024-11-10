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
                ProcessImage(320, 240, fileName);
            }
            return fileName;
        }
        public void ProcessImage(int maxWidth, int maxHeight, string filename)
        {
            filename = Path.Combine(_baseDirectory, filename);

            using (var imageColl = new MagickImageCollection(filename))
            {
                int widthOrig = (int)imageColl[0].Width;
                int heightOrig = (int)imageColl[0].Height;

                imageColl.Coalesce();
                imageColl.Optimize();
                imageColl.OptimizeTransparency();

                foreach (MagickImage image in imageColl)
                {
                    var (width, height) = GetNewSize(widthOrig, heightOrig, maxWidth, maxHeight);
                    image.Resize((uint)width, (uint)height);
                }

                imageColl.Write(filename);
            }
        }

        private (int width, int height) GetNewSize(int originalWidth, int originalHeight, int maxWidth, int maxHeight)
        {
            decimal ratio = originalWidth / (decimal)originalHeight;
            decimal newRatio = maxWidth / (decimal)maxHeight;
            if (ratio > 1 && newRatio < 1 || ratio < 1 && newRatio > 1)
                (maxWidth, maxHeight) = (maxHeight, maxWidth);
            decimal widthRatio = (decimal)maxWidth / originalWidth;
            decimal heightRatio = (decimal)maxHeight / originalHeight;

            decimal scaleRatio = Math.Min(widthRatio, heightRatio);

            int newWidth = (int)Math.Round(originalWidth * scaleRatio);
            int newHeight = (int)Math.Round(originalHeight * scaleRatio);

            return (newWidth, newHeight);
        }

    }
}
