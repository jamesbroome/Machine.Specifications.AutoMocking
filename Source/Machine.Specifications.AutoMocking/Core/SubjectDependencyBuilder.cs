namespace Machine.Specifications.AutoMocking.Core
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    #endregion

    public class SubjectDependencyBuilder : ISubjectDependencyBuilder
    {
        readonly IDependencyBag dependency_bag;
        readonly IMockFactory mock_factory;

        public SubjectDependencyBuilder(IDependencyBag dependency_bag, IMockFactory mock_factory)
        {
            this.dependency_bag = dependency_bag;
            this.mock_factory = mock_factory;
        }

        #region ISystemUnderTestDependencyBuilder Members

        public void register_only_if_missing(Type dependency_type)
        {
            if (this.dependency_needs_to_be_registered_for(dependency_type))
                this.dependency_bag.register_dependency_for_subject(dependency_type,
                                                                this.mock_factory.create_stub(dependency_type));
        }

        public Dependency dependency_of<Dependency>() where Dependency : class
        {
            if (this.dependency_bag.has_no_dependency_for<Dependency>())
                this.dependency_bag.store_dependency(typeof(Dependency), this.mock_factory.create_stub<Dependency>());
            return this.dependency_bag.get_dependency<Dependency>();
        }

        public void provide_a_basic_subject_constructor_argument<ArgumentType>(ArgumentType value)
        {
            this.store_a_subject_constructor_argument<ArgumentType>(value);
        }

        public object[] all_dependencies(IEnumerable<Type> constructor_parameter_types)
        {
            return
                constructor_parameter_types.Select(
                    parameter => this.dependency_bag.get_the_provided_dependency_assignable_from(parameter)).
                    ToArray();
        }

        #endregion

        bool dependency_needs_to_be_registered_for(Type dependency_type)
        {
            return this.dependency_bag.has_no_dependency_for(dependency_type) &&
                   this.is_a_depedency_that_can_automatically_be_created(dependency_type);
        }

        bool is_a_depedency_that_can_automatically_be_created(Type dependency_type)
        {
            return ! dependency_type.IsValueType;
        }

        void store_a_subject_constructor_argument<ArgumentType>(ArgumentType argument)
        {
            this.ensure_the_dependency_has_not_already_been_register<ArgumentType>();
            this.dependency_bag.store_dependency(typeof(ArgumentType), argument);
        }

        void ensure_the_dependency_has_not_already_been_register<ArgumentType>()
        {
            if (! this.dependency_bag.has_no_dependency_for<ArgumentType>())
                throw new ArgumentException(
                    string.Format("A dependency has already been provided for :{0}", typeof(ArgumentType).proper_name()));
        }
    }
}