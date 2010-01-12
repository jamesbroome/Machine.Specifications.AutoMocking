namespace Machine.Specifications.AutoMocking.Core
{
    using System;

    public interface IDependencyBag
    {
        void store_dependency(Type type, object instance);
        Dependency get_dependency<Dependency>();
        bool has_no_dependency_for<Dependency>();
        void register_dependency_for_subject(Type dependency_type, object instance);
        bool has_no_dependency_for(Type dependency_type);
        object get_the_provided_dependency_assignable_from(Type constructor_parament_type);
    }
}