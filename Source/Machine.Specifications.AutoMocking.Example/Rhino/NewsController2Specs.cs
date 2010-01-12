using Machine.Specifications;
using Machine.Specifications.AutoMocking.Rhino;
using Rhino.Mocks;

namespace Machine.Specifications.AutoMocking.Example.Rhino
{
    /// <summary>
    /// Example specification for a class without a contract that manually creates subject
    /// </summary>
    public abstract class context_for_news_controller_2 : Specification<NewsController2>
    {
        protected static INewsService newsService;

        Establish context = () => newsService = An<INewsService>(); // An<> provides easy way to create a mock instance
 
        protected override NewsController2 CreateSubject()
        {
            return new NewsController2(newsService); // the CreateSubject can be override if necessary
        }
    }

    [Subject(typeof(NewsController2))]
    public class when_the_news_controller_2_is_told_to_display_the_default_view : context_for_news_controller_2
    {
        static string result;

        Establish context = () => newsService.Stub(x => x.GetLatestHeadline()).Return("The latest headline");

        Because of = () => result = subject.Index(); // the subject has been created for us automatically, with all registered dependencies

        It should_ask_the_news_service_for_the_latest_headline =
            () => newsService.AssertWasCalled(x => x.GetLatestHeadline());

        It should_display_the_latest_headline = () => result.ShouldEqual("The latest headline");
    }
}