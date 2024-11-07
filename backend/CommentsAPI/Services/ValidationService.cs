using CommentsAPI.Interfaces;
using HtmlAgilityPack;

namespace CommentsAPI.Services
{
    public class ValidationService : IValidationService
    {
        private readonly string[] _allowedTags = new[] { "a", "code", "i", "strong" };
        public bool IsValidHtml(string input)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(input);

            var nodes = doc.DocumentNode.Descendants().Where(n => n.NodeType == HtmlNodeType.Element);

            foreach (var node in nodes)
            {
                if (!_allowedTags.Contains(node.Name))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
