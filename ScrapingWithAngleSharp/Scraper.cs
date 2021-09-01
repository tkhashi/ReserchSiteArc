using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ScrapingWithAngleSharp.Data;

namespace ScrapingWithAngleSharp
{
    public class Scraper
    {
        public string BaseUrl { get; }
        public PageData PageData { get; }
        public int Hierarchy { get; }

        public Scraper(string baseUrl, int hierarchy)
        {
            BaseUrl = baseUrl;
            PageData = Scrape().Result;
            Hierarchy = hierarchy;
        }

        public async Task<PageData> Scrape()
        {
            //設定？
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            //対象のアドレスをconfigの設定で開く？
            var baseUrl = BaseUrl;
            if (!Regex.IsMatch(baseUrl, @"$/")) baseUrl += @"/";
            var domain = Regex.Match(baseUrl, @"https?://[^/]+/?").Value;
            var http = Regex.Match(baseUrl, @"https?:").Value;
            var document = await context.OpenAsync(baseUrl);
            //対象の要素・属性を取得
            var links = document.GetAHref();
            var frameLinks = document.GetFrameLink();

            var flagRegex = @"^#";
            var relativeRegex = @"^/[^http][^#]";
            var fullPathRegex = @"^http";
            var urlList = new List<string>()
                //.AddFlagUrls(baseUrl, links, flagRegex)
                .AddRelativePathUrls(domain, links, relativeRegex)
                .AddFullPathUrls(links, fullPathRegex);

            //foreach (var url in urlList)
            //{
            //    Console.WriteLine(url);
            //}

            var urlListInFrame = new List<string>()
                .AddRelativePathUrls(domain, frameLinks, relativeRegex)
                .Select(async frameUrl =>
                {
                    var documentInFrame = await context.OpenAsync(frameUrl);
                    var linksInFrame = documentInFrame.GetAHref();

                    return new List<string>()
                        //.AddFlagUrls(baseUrl, linksInFrame, flagRegex)
                        .AddRelativePathUrls(domain, linksInFrame, relativeRegex)
                        .AddFullPathUrls(linksInFrame, fullPathRegex);
                })
                .SelectMany(x => x.Result);

            //foreach (var url in urlListInFrame)
            //{
            //    Console.WriteLine(url);
            //}

            var title = document.GetTitle();
            var childrenUrls = urlList.Concat(urlListInFrame);

            return new PageData(baseUrl, title, childrenUrls);
        }
    }
}