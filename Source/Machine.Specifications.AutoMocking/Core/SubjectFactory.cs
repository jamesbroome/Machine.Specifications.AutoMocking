namespace Machine.Specifications.AutoMocking.Core
{
    #region Using Directives

    using System;
    using System.Linq;
    using Extensions;
    using Utility;

    #endregion

    public class SubjectFactory : ISubjectFactory
    {
        readonly ISubjectDependencyBuilder dependency_builder;

        public SubjectFactory(ISubjectDependencyBuilder dependency_builder)
        {
            this.dependency_builder = dependency_builder;
        }

        #region ISubjectFactory Members

        public Contract create<Contract, Class>()
        {
            var constructor = typeof(Class).greediest_constructor();
            var constructor_parameter_types =
                constructor.GetParameters().Select(constructor_arg => constructor_arg.ParameterType);
            constructor_parameter_types.Each(this.dependency_builder.register_only_if_missing);
            
            return (Contract) Activator.CreateInstance(typeof(Class),
                                         this.dependency_builder.all_dependencies(constructor_parameter_types));
        }

        #endregion
    }
}