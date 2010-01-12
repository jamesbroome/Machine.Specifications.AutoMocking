namespace Machine.Specifications.AutoMocking
{
    using Core;
    using Core.Observations;

    public abstract class Specification<TContract, TSubject, TMockFactoryAdapter> where TSubject : TContract
                                                                                       where TMockFactoryAdapter :
                                                                                           IMockFactory, new()
    {
        static ITestState<TContract> test_state;

        protected Specification()
        {
            test_state = new TestState<TContract>(this, this.CreateSubject);

            var args = new ObservationContextArgs<TContract>
                           {
                               mock_factory = new TMockFactoryAdapter(),
                               state = test_state,
                               test = this
                           };

            observation_context = new ObservationContextFactory().create_from(args);
        }

        static IObservationContext observation_context
        {
            get;
            set;
        }

        protected static TContract subject
        {
            get
            {
                if (test_state.subject == null)
                {
                    test_state.build_subject();
                }

                return test_state.subject;
            }
        }

        protected virtual TContract CreateSubject()
        {
            return observation_context.build_subject<TContract, TSubject>();
        }

        protected static Dependency DependencyOf<Dependency>() where Dependency : class
        {
            return observation_context.dependency_of<Dependency>();
        }

        protected static InterfaceType An<InterfaceType>() where InterfaceType : class
        {
            return observation_context.an<InterfaceType>();
        }

        protected static void ProvideBasicConstructorArgument<ArgumentType>(ArgumentType value)
        {
            observation_context.provide_a_basic_subject_constructor_argument(value);
        }
    }
}