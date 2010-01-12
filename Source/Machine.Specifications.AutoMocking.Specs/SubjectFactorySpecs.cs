namespace Machine.Specifications.AutoMocking.Specs
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Core;
    using global::Rhino.Mocks;
    using Rhino;

    #endregion

    [Subject(typeof(SubjectFactory))]
    public abstract class context_for_subject_factory : Specification<ISubjectFactory, SubjectFactory>
    {
        static ISubjectDependencyBuilder builder;
        protected static IDbCommand command;
        protected static IDbConnection connection;

        Establish context = () =>
            {
                connection = MockRepository.GenerateStub<IDbConnection>();
                command = MockRepository.GenerateStub<IDbCommand>();
                builder = MockRepository.GenerateStub<ISubjectDependencyBuilder>();

                builder.Stub(x =>
                             x.all_dependencies(Arg<IEnumerable<Type>>.Matches(
                                                    args =>
                                                    Enumerable.First<Type>(args) == typeof(IDbCommand) &&
                                                    Enumerable.Skip<Type>(args, 1).Take(1).First() ==
                                                    typeof(IDbConnection))))
                    .Return(new object[] { command, connection });
            };

        protected override ISubjectFactory CreateSubject()
        {
            return new SubjectFactory(builder);
        }
    }

    [Subject(typeof(SubjectFactory))]
    public class when_creating_the_subject : context_for_subject_factory
    {
        static ISubjectDependencyBuilder builder;
        static AClassWithDependencies result;

        Because of = () => result = subject.create<AClassWithDependencies, AClassWithDependencies>();

        It should_create_an_instance_of_the_system_under_test_using_the_builders_constructor_arg_array = () =>
            {
                result.ShouldNotBeNull();
                result.command.ShouldEqual(command);
                result.connection.ShouldEqual(connection);
            };
    }

    public class AClassWithDependencies
    {
        public AClassWithDependencies(IDbCommand command, IDbConnection connection)
        {
            this.command = command;
            this.connection = connection;
        }

        public IDbCommand command
        {
            get;
            private set;
        }

        public IDbConnection connection
        {
            get;
            private set;
        }
    }
}