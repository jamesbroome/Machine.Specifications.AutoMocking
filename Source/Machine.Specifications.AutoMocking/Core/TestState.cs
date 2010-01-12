namespace Machine.Specifications.AutoMocking.Core
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    public class TestState<Subject> : ITestState<Subject>
    {
        public TestState(object test, Func<Subject> factory)
        {
            this.test = test;
            this.factory = factory;

            this.dependencies = new Dictionary<Type, object>();
        }

        object test
        {
            get;
            set;
        }

        IDictionary<Type, object> dependencies
        {
            get;
            set;
        }

        Func<Subject> factory
        {
            get;
            set;
        }

        #region ITestState<Subject> Members

        public Subject subject
        {
            get;
            set;
        }

        public void build_subject()
        {
            this.subject = this.factory();
        }

        public void store_dependency(Type type, object instance)
        {
            this.dependencies.Add(type, instance);
        }

        public Dependency get_dependency<Dependency>()
        {
            return (Dependency) this.dependencies[typeof(Dependency)];
        }

        public bool has_no_dependency_for<Interface>()
        {
            return this.has_no_dependency_for(typeof(Interface));
        }

        public bool has_no_dependency_for(Type dependency_type)
        {
            return ! this.dependencies.ContainsKey(dependency_type);
        }

        public void register_dependency_for_subject(Type dependency_type, object instance)
        {
            this.dependencies[dependency_type] = instance;
        }

        public object get_the_provided_dependency_assignable_from(Type constructor_parament_type)
        {
            return this.dependencies[constructor_parament_type];
        }

        #endregion
    }
}