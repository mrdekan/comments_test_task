using CommentsAPI.Interfaces;
using System.Text.RegularExpressions;
using System.Xml;

namespace CommentsAPI.Services
{
    public class ValidationService : IValidationService
    {
        private readonly string[] _allowedTags = new[] { "a", "code", "i", "strong" };
        private readonly string[] _allowedFileExtensions = new[] { ".txt", ".png", ".jpg", ".gif" };
        private const int MAX_TXT_FILE_SIZE_IN_KB = 100;
        private const double BYTES_IN_KB = 1024.0;
        public bool IsValidHtml(string input)
        {
            string xhtmlWrapper = $"<code xmlns=\"http://www.w3.org/1999/xhtml\">{input}</code>";

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xhtmlWrapper);

                var allowedTags = new[] { "a", "code", "i", "strong" };
                var nodes = doc.GetElementsByTagName("*");

                foreach (XmlNode node in nodes)
                {
                    if (!allowedTags.Contains(node.Name))
                    {
                        return false;
                    }
                    if (node.Name == "a")
                    {
                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            if (attribute.Name != "title" && attribute.Name != "href")
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (node.Attributes.Count > 1)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        public bool IsValidTxtFile(IFormFile formFile)
        {
            return formFile.Length / BYTES_IN_KB <= MAX_TXT_FILE_SIZE_IN_KB;
        }

        public bool IsValidFileExtension(IFormFile formFile)
        {
            return _allowedFileExtensions.Contains(Path.GetExtension(formFile.FileName));
        }

        public bool IsValidURL(string url)
        {
            string urlPattern = @"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$";
            return Regex.IsMatch(url, urlPattern);
        }

        public bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
