namespace Machine.Specifications.AutoMocking.Example
{
    using System.Collections.Generic;
    using System.Linq;

    public class NewsService : INewsService
    {
        readonly List<string> headlines;

        public NewsService(List<string> headlines)
        {
            this.headlines = headlines;
        }

        public string GetLatestHeadline()
        {
            return this.headlines.Last();
        }

        public List<string> GetAllHeadlines()
        {
            return this.headlines;
        }
    }
}