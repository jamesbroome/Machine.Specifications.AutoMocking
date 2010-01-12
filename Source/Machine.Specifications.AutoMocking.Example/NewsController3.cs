namespace Machine.Specifications.AutoMocking.Example
{
    public class NewsController3
    {
        public INewsService NewsService
        {
            get;
            set;
        }

        public string Index()
        {
            return this.NewsService.GetLatestHeadline();
        }
    }
}