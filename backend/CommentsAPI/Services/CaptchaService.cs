using CommentsAPI.Interfaces;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace CommentsAPI.Services
{
    public class CaptchaService : ICaptchaService
    {
        private const int WIDTH = 200;
        private const int HEIGHT = 100;
        public Image GenerateImage(string captchaText)
        {
            var image = new Image<Rgba32>(WIDTH, HEIGHT);

            var font = SystemFonts.CreateFont("Arial", 24);

            image.Mutate(ctx => ctx.Fill(new Rgba32(211, 211, 211)));

            var xPos = 10;
            var yPos = 30;
            var drawingOptions = new DrawingOptions()
            {
            };
            var textOptions = new TextOptions(font)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            foreach (char c in captchaText)
            {
                var random = new Random();
                var textColor = new Rgba32((byte)random.Next(0, 90), (byte)random.Next(0, 90), (byte)random.Next(0, 90));

                image.Mutate(ctx =>
                {
                    ctx.DrawText(drawingOptions, c.ToString(), font, textColor, new PointF(xPos, yPos));
                });

                xPos += random.Next(20, 30);
                yPos += random.Next(-5, 5);
            }

            for (int i = 0; i < 35; i++)
            {
                var random = new Random();
                var lineX1 = random.Next(0, WIDTH);
                var lineY1 = random.Next(0, HEIGHT);
                var lineX2 = random.Next(0, WIDTH);
                var lineY2 = random.Next(0, HEIGHT);
                var lineColor = new Rgba32((byte)random.Next(70, 255), (byte)random.Next(70, 255), (byte)random.Next(70, 255));
                image.Mutate(ctx => ctx.DrawLine(lineColor, 1, new PointF(lineX1, lineY1), new PointF(lineX2, lineY2)));
            }

            return image;
        }

        public string GenerateRandomText()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
