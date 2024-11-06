using Microsoft.AspNetCore.Mvc;

namespace CommentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCaptcha()
        {
            string captchaText = GenerateRandomText();
            using var image = CaptchaGen.ImageFactory.GenerateImage(captchaText);
            HttpContext.Session.SetString("captchaText", captchaText);
            return File(image.ToArray(), "image/png");
        }

        private string GenerateRandomText()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
