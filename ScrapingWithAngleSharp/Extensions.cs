using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Dom;

namespace ScrapingWithAngleSharp
{
    public static class Extensions
    {
        public static IEnumerable<string> GetElementByAttr(this IDocument document, string tag, string attr)
        {
            return document
                    .QuerySelectorAll(tag)
                    .Select(cell => cell.GetAttribute(attr));
        }

        public static string GetFullPath(this string domain, string path)
        {
            return domain + path;
        }

        public static List<string> AddFlagUrls(this List<string> urlList, string baseUrl, IEnumerable<string> links,
            string regex)
        {
            var addLinks = links
                .Where(link => link != null)
                .Where(link => Regex.IsMatch(link, regex))
                .Select(link => baseUrl.GetFullPath(link))
                .ToList();
            urlList.AddRange(addLinks);

            return urlList;
        }

        public static List<string> AddRelativePathUrls(this List<string> urlList, string domain,
            IEnumerable<string> links, string regex)
        {
            var addLinks = links
                .Where(link => link != null)
                .Where(link => Regex.IsMatch(link, regex))
                .Select(link => domain.GetFullPath(link))
                .ToList();

            urlList.AddRange(addLinks);

            return urlList;
        }

        public static List<string> AddFullPathUrls(this List<string> urlList, IEnumerable<string> links, string regex)
        {
            var addLinks = links
                .Where(link => link != null)
                .Where(link => Regex.IsMatch(link, regex))
                .ToList();

            urlList.AddRange(addLinks);

            return urlList;
        }
    }
}