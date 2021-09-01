using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace ScrapingWithAngleSharp
{
    public class Controller
    {
        public List<Scraping> Scrapers { get; }

        public Controller()
        {
            Scrapers = new List<Scraping>();
        }

        public void AddScraper(Scraping scraper)
        {
            Scrapers.Add(scraper);
        }
    }
}