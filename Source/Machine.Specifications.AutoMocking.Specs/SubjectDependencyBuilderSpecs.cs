namespace Machine.Specifications.AutoMocking.Specs
{
    #region Using Directives

    using System;
    using System.Data;
    using Core;
    using global::Rhino.Mocks;
    using Rhino;

    #endregion

    [Subject(typeof(SubjectDependencyBuilder))]
    public abstract class context_for_a_subject_dependency_builder :
        Specification<ISubjectDependencyBuilder, SubjectDependencyBuilder>
    {
        protected static IDependencyBag dependency_bag;
        protected static IMockFactory mock_factory;

        Establish context = () =>
            {
                dependency_bag = An<IDependencyBag>();
                mock_factory = An<IMockFactory>();
            };

        protected override ISubjectDependencyBuilder CreateSubject()
        {
            return new SubjectDependencyBuilder(dependency_bag, mock_factory);
        }
    }

    [Subject(typeof(SubjectDependencyBuilder))]
    public class when_requesting_a_dependency : context_for_a_subject_dependency_builder
    {
        protected static IDbConnection connection;
        protected static IDbConnection result;

        Establish context = () =>
            {
                connection = An<IDbConnection>();
                mock_factory.Stub(x => x.create_stub<IDbConnection>()).Return(connection);
            };

        Because of = () => result = subject.dependency_of<IDbConnection>();
    }

    [Subject(typeof(SubjectDependencyBuilder))]
    public class and_the_dependencies_have_not_already_been_provided : when_requesting_a_dependency
    {
        Establish context = () =>
            {
                dependency_bag.Stub(x => x.has_no_dependency_for<IDbConnection>()).Return(true);
                dependency_bag.Stub(x => x.get_dependency<IDbConnection>()).Return(connection);
            };

        It should_return_the_dependency_and_store_it_in_the_dependencies_dictionary = () =>
            {
                result.ShouldEqual(connection);
                dependency_bag.AssertWasCalled(x => x.store_dependency(typeof(IDbConnection), connection));
            };
    }

    [Subject(typeof(SubjectDependencyBuilder))]
    public class and_the_dependencies_have_been_provided : when_requesting_a_dependency
    {
        Establish context = () =>
            {
                dependency_bag.Stub(x => x.has_no_dependency_for<IDbConnection>()).Return(false);
                dependency_bag.Stub(x => x.get_dependency<IDbConnection>()).Return(connection);
            };

        It should_not_restore_the_dependency_and_return_the_dependency = () =>
            {
                result.ShouldEqual(connection);
                dependency_bag.AssertWasNotCalled(x => x.store_dependency(typeof(IDbConnection), connection));
            };
    }

    [Subject(typeof(SubjectDependencyBuilder))]
    public class when_providing_a_basic_constructor_argument : context_for_a_subject_dependency_builder
    {
        protected static ClassWithoutAContract class_without_a_contract;
        protected static bool exception_thrown;

        Establish context = () =>
            {
                class_without_a_contract = new ClassWithoutAContract();
            };

        Because of = () =>
            {
                try
                {
                    subject.provide_a_basic_subject_constructor_argument(class_without_a_contract);
                }
                catch (ArgumentException)
                {
                    exception_thrown = true;
                }
            };
    }

    [Subject(typeof(SubjectDependencyBuilder))]
    public class and_an_argument_has_not_already_been_provided : when_providing_a_basic_constructor_argument
    {
        Establish context = () => dependency_bag.Stub(x => x.has_no_dependency_for<ClassWithoutAContract>()).Return(true);

        It should_store_the_argument_in_the_dependencies_dictionary = () =>
            dependency_bag.AssertWasCalled(x => x.store_dependency(typeof(ClassWithoutAContract), class_without_a_contract));
    }

    [Subject(typeof(SubjectDependencyBuilder))]
    public class and_an_argument_has_already_been_provided : when_providing_a_basic_constructor_argument
    {
        Establish context = () => dependency_bag.Stub(x => x.has_no_dependency_for<ClassWithoutAContract>()).Return(false);

        It should_throw_an_exception = () => exception_thrown.ShouldBeTrue();
    }

    public class ClassWithoutAContract
    {
    }
}