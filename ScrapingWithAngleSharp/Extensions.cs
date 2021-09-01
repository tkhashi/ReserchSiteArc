using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Dom;

namespace ScrapingWithAngleSharp
{
    public static class Extensions
    {
        public static IEnumerable<string> GetFrameLink(this IDocument document)
        {
            var href = document
                .QuerySelectorAll("frame")
                .Select(cell => cell.GetAttribute("src"));
            return href;
        }
        public static IEnumerable<string> GetAHref(this IDocument document)
        {
            var href = document
                .QuerySelectorAll("a")
                .Select(cell => cell.GetAttribute("href"));
            return href;
        }

        public static string GetTitle(this IDocument document)
        {
            var title = document.QuerySelector("title").TextContent != null? document.GetElementsByClassName("title").ToString();
            return title;
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