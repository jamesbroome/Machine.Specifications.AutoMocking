namespace Machine.Specifications.AutoMocking.Core.Observations
{
    #region Using Directives

    

    #endregion

    public class ObservationContext<Subject> : IObservationContext
    {
        public ObservationContext(ITestState<Subject> test_state_implementation,
                                  ISubjectDependencyBuilder subject_dependency_builder,
                                  IMockFactory mock_factory,
                                  ISubjectFactory subject_factory)
        {
            this.mock_factory = mock_factory;
            this.test_state = test_state_implementation;
            this.subject_dependency_builder = subject_dependency_builder;
            this.subject_factory = subject_factory;
        }

        ITestState<Subject> test_state
        {
            get;
            set;
        }

        IMockFactory mock_factory
        {
            get;
            set;
        }

        ISubjectDependencyBuilder subject_dependency_builder
        {
            get;
            set;
        }

        ISubjectFactory subject_factory
        {
            get;
            set;
        }

        #region IObservationContext Members

        public Contract build_subject<Contract, Class>()
        {
            return this.subject_factory.create<Contract, Class>();
        }

        public Dependency dependency_of<Dependency>() where Dependency : class
        {
            return this.subject_dependency_builder.dependency_of<Dependency>();
        }

        public void provide_a_basic_subject_constructor_argument<ArgumentType>(ArgumentType value)
        {
            this.subject_dependency_builder.provide_a_basic_subject_constructor_argument(value);
        }

        public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return this.mock_factory.create_stub<InterfaceType>();
        }

        #endregion
    }
}