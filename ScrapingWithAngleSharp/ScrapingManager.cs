using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AngleSharp.Css;
using ScrapingWithAngleSharp.Data;

namespace ScrapingWithAngleSharp
{
    /// <summary>
    /// Scraping such a site and children site and children and ..
    /// </summary>
    public class ScrapingManager
    {
        public int Roop { get; }
        public string BaseUrl { get; }

        public ScrapingManager(int roop)
        {
            Roop = roop;
            BaseUrl = "https://www.gesuidouten.jp/top/index/";
            //親URLのスクレイピング
            StartScraping(BaseUrl, 1);
        }

        public void StartScraping(string baseUrl, int hierarchy)
        {
            for (int i = 0; hierarchy <= Roop; hierarchy++)
            {
                var scraper = new Scraper(baseUrl, hierarchy);
                Console.WriteLine("このページは「" + $"{scraper.PageData.Title}" + "」です。");
                Console.WriteLine($"URLは「{scraper.BaseUrl}」です。");

                var children = scraper.PageData.ChildrenUrls;
                foreach (var url in scraper.PageData.ChildrenUrls)
                {
                    Console.WriteLine($"{scraper.PageData.Title}の子供" + url);
                }

                foreach (var url in children)
                {
                    StartScraping(url, hierarchy);
                }
            }

        }
    }
}