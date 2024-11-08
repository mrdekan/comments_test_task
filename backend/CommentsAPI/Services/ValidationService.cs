using CommentsAPI.Interfaces;
using System.Xml;

namespace CommentsAPI.Services
{
    public class ValidationService : IValidationService
    {
        private readonly string[] _allowedTags = new[] { "a", "code", "i", "strong" };
        public bool IsValidHtml(string input)
        {
            string xhtmlWrapper = $"<div xmlns=\"http://www.w3.org/1999/xhtml\">{input}</div>";

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xhtmlWrapper);

                var allowedTags = new[] { "a", "code", "i", "strong", "div" }; // div - для обёртки
                var nodes = doc.GetElementsByTagName("*");

                foreach (XmlNode node in nodes)
                {
                    if (!allowedTags.Contains(node.Name))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }
    }
}
