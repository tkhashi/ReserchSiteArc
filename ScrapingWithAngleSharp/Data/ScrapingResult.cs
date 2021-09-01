using System.Collections.Generic;

namespace ScrapingWithAngleSharp.Data
{
    public class ScrapingResult
    {
        public int Hierarchy { get; }
        public IEnumerable<PageData> PageDatas { get; }
        public string ScreenShotPath { get; }

        public ScrapingResult(int hierarchy, IEnumerable<PageData> pageDatas)
        {
            Hierarchy = hierarchy;
            PageDatas = pageDatas;
        }
}
}