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
        static void Main(string[] args)
        {
            new ScrapingManager(1);
        }
    }
}