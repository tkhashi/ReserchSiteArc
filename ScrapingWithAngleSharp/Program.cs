using System;
using System.Collections.Generic;
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
            var baseUrl = "https://blog.okazuki.jp/entry/2017/07/23/165809";
            var domain = Regex.Match(baseUrl, @"https?://[^/]+/?").Value;
            var http = Regex.Match(baseUrl, @"https?:").Value;
            var document = await context.OpenAsync(baseUrl);
            //対象の要素を取得
            var aTag = "a";
            var cells = document.QuerySelectorAll(aTag);
            //取得した要素のhref属性を取得
            var links = cells.Select(cell => cell.GetAttribute("href"));

            var flagUrls = links
                .Where( link => link != null)
                .Where(link => Regex.IsMatch(link, @"^#"))
                .Select(link => baseUrl.GetFullPath(link))
                .ToList();
            var relativePaths = links
                .Where( link => link != null)
                .Where(link => Regex.IsMatch(link, @"^[^/][^http][^#]"))
                .Select(link => domain.GetFullPath(link))
                .ToList();
            var fullPaths = links
                .Where( link => link != null)
                .Where(link => Regex.IsMatch(link, @"^http"))
                .ToList();

            var urlList = new List<string>();
            urlList.AddRange(flagUrls);
            urlList.AddRange(relativePaths);
            urlList.AddRange(fullPaths);

            foreach (var url in urlList)
            {
                Console.WriteLine(url);
            }

        }

    }
}
