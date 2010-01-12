using Machine.Specifications;
using Machine.Specifications.AutoMocking.Rhino;
using System.Collections.Generic;

namespace Machine.Specifications.AutoMocking.Example.Rhino
{
    /// <summary>
    /// Example specification for a class with a contract that uses constructor based DI
    /// </summary>
    public abstract class context_for_news_service : Specification<INewsService, NewsService>
    {
        Establish context = () =>
            {
                var newsHeadlines = new List<string> {"Yesterday's headline", "Today's headline"};
            
                ProvideBasicConstructorArgument(newsHeadlines); // manually add a required simple constructor argument
            };
    }

    [Subject(typeof(NewsService))]
    public class when_the_news_service_is_asked_for_the_latest_headline : context_for_news_service
    {
        static string result;

        Because of = () => result = subject.GetLatestHeadline(); // subject is created automatically and returned as an INewsService so we are coding against the interface

        It should_return_the_latest_headline = () => result.ShouldEqual("Today's headline");
    }

    [Subject(typeof(NewsService))]
    public class when_the_news_service_is_asked_for_all_the_headlines : context_for_news_service
    {
        static List<string> result;

        Because of = () => result = subject.GetAllHeadlines();

        It should_return_the_list_of_all_headlines = () => result.Count.ShouldEqual(2);
    }
}