using SixLabors.ImageSharp;
namespace CommentsAPI.Interfaces
{
    public interface ICaptchaService
    {
        Image GenerateImage(string captchaText);
        string GenerateRandomText();
    }
}
