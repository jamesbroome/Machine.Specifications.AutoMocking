namespace Machine.Specifications.AutoMocking.Example
{
    public class NewsController2
    {
        readonly INewsService newsService;

        public NewsController2(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public string Index()
        {
            return this.newsService.GetLatestHeadline();
        }
    }
}