namespace Machine.Specifications.AutoMocking.Core
{
    #region Using Directives

    using System;
    using System.Collections.Generic;

    #endregion

    public interface ISubjectDependencyBuilder
    {
        Dependency dependency_of<Dependency>() where Dependency : class;
        void provide_a_basic_subject_constructor_argument<ArgumentType>(ArgumentType value);
        object[] all_dependencies(IEnumerable<Type> enumerable);
        void register_only_if_missing(Type dependency_type);
    }
}