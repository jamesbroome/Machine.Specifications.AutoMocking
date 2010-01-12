namespace Machine.Specifications.AutoMocking.Example
{
    public class NewsController
    {
        readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public string Index()
        {
            return this.newsService.GetLatestHeadline();
        }
    }
}