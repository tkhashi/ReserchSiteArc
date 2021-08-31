namespace ScrapingWithAngleSharp
{
    public static class Extensions
    {
        public static string GetFullPath(this string domain, string path)
        {
            return domain + path;
        }
        
    }
}