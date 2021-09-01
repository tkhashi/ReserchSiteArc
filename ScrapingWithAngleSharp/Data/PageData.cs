using System;
using System.Collections.Generic;

namespace ScrapingWithAngleSharp.Data
{
    public class PageData
    {
        public string Id { get; }
        public string Url { get; }
        public string Title { get; }
        public IEnumerable<string> ChildrenUrls { get; }

        public PageData(string url, string title, IEnumerable<string> childrenUrls)
        {
            Url = url;
            Title = title;
            ChildrenUrls = childrenUrls;

            Id = Guid.NewGuid().ToString("N");
        }
    }
}