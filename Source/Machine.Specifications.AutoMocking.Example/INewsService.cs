namespace Machine.Specifications.AutoMocking.Example
{
    using System.Collections.Generic;

    public interface INewsService
    {
        string GetLatestHeadline();
        List<string> GetAllHeadlines();
    }
}