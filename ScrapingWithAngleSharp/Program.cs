using System;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Common;
using AngleSharp.Dom;

namespace ScrapingWithAngleSharp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //設定？
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            //対象のアドレスをconfigの設定で開く？
            var baseUrl = "https://en.wikipedia.org/wiki/List_of_The_Big_Bang_Theory_episodes";
            var domain = Regex.Match(baseUrl, @"(https?://[^/]+/)").Value;
            var http = Regex.Match(baseUrl, @"https?:").Value;
            var document = await context.OpenAsync(baseUrl);

            //対象の要素を取得
            var aTag = "a";
            var cells = document.QuerySelectorAll(aTag);
            //取得した要素のhref属性を取得
            var links = cells.Select(cell => cell.GetAttribute("href")).Where(href => href == "#cite_note-4");

            var flagUrl = links
                .Where(link => Regex.IsMatch(link, @"^#"))
                .Select(link => domain.GetFullPath(link));
            var relativePath = links
                .Where(link => Regex.IsMatch(link, @"^[^/][^http][^#]"))
                .Select(link => domain.GetFullPath(link));
            var fullPath = links
                .Where(link => Regex.IsMatch(link, @"^http"));

            //foreach (var link in fullLinks)
            //{
            //    Console.WriteLine(link);
            //}

        }

    }
}
