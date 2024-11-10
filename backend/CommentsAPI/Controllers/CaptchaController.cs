using CommentsAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Png;


namespace CommentsAPI.Controllers
{
    [Route("api/captcha")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptchaService _captchaService;
        public CaptchaController(ICaptchaService captchaService)
        {
            _captchaService = captchaService;
        }

        [HttpGet]
        public IActionResult GetCaptcha()
        {
            string captchaText = _captchaService.GenerateRandomText();
            var image = _captchaService.GenerateImage(captchaText);

            HttpContext.Session.SetString("captchaText", captchaText);

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, new PngEncoder());
                return File(memoryStream.ToArray(), "image/png");
            }
        }
    }
}
