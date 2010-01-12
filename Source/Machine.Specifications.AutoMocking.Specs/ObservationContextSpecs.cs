namespace Machine.Specifications.AutoMocking.Specs
{
    #region Using Directives

    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Core;
    using Core.Observations;
    using global::Rhino.Mocks;
    using Rhino;

    #endregion

    [Subject(typeof(ObservationContext<>))]
    public abstract class context_for_observation_context
    {
        static ISubjectDependencyBuilder dependency_builder;
        protected static Func<IDbConnection> factory;
        protected static IMockFactory mock_factory;
        protected static ObservationContext<IDbConnection> subject;
        static ISubjectFactory subject_factory;
        protected static SampleSetOfObservations test_driver;
        protected static ITestState<IDbConnection> test_state;

        Establish context = () =>
            {
                factory = () => new SqlConnection();
                test_driver = new SampleSetOfObservations();
                subject_factory = MockRepository.GenerateStub<ISubjectFactory>();
                dependency_builder = MockRepository.GenerateStub<ISubjectDependencyBuilder>();
                mock_factory = MockRepository.GenerateStub<IMockFactory>();
                test_state = new TestState<IDbConnection>(test_driver, factory);

                subject = create_the_subject();
            };

        static ObservationContext<IDbConnection> create_the_subject()
        {
            return new ObservationContext<IDbConnection>(test_state, dependency_builder, mock_factory, subject_factory);
        }
    }

    public class SampleSetOfObservations : Specification<IDbConnection>
    {
        protected override IDbConnection CreateSubject()
        {
            return DependencyOf<IDbConnection>();
        }
    }

    [Subject(typeof(ObservationContext<>))]
    public class when_creating_a_mock : context_for_observation_context
    {
        static IDbConnection connection;
        static IDbConnection result;

        Establish context = () =>
            {
                connection = MockRepository.GenerateStub<IDbConnection>();
                mock_factory.Stub(x => x.create_stub<IDbConnection>()).Return(connection);
                mock_factory.Stub(x => x.create_stub(typeof(IDbConnection))).Return(connection);
            };

        Because of = () => result = subject.an<IDbConnection>();

        It should_return_the_mocks_created_by_the_mock_factory = () => result.ShouldEqual(connection);
    }
}