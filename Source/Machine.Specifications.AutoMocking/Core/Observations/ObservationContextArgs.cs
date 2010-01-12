namespace Machine.Specifications.AutoMocking.Core.Observations
{
    #region Using Directives

    

    #endregion

    public class ObservationContextArgs<Contract>
    {
        public ITestState<Contract> state
        {
            get;
            set;
        }

        public IMockFactory mock_factory
        {
            get;
            set;
        }

        public object test
        {
            get;
            set;
        }
    }
}