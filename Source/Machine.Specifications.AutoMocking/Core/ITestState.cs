namespace Machine.Specifications.AutoMocking.Core
{
    public interface ITestState<Subject> : IDependencyBag
    {
        Subject subject
        {
            get;
            set;
        }

        void build_subject();
    }
}