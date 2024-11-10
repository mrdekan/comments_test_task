namespace CommentsAPI.Interfaces
{
    public interface IValidationService
    {
        bool IsValidHtml(string input);
        bool IsValidTxtFile(IFormFile formFile);
        bool IsValidFileExtension(IFormFile formFile);
        bool IsValidEmail(string email);
        bool IsValidURL(string url);
    }
}
