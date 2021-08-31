
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using AngleSharp;

namespace ScrapingWithAngleSharp
{
    public class AngleSharpHtmlParser
    {
        private HttpClient httpClient { get; } = new();
        private HtmlParser htmlParser { get; } = new();

        public async void Load()
        {
        }
    }
}