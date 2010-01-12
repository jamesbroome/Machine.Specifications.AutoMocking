namespace Machine.Specifications.AutoMocking.Specs
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using Core.Extensions;

    #endregion

    [Subject(typeof(TypeExtensions))]
    public class when_a_type_is_told_to_find_its_greediest_constructor 
    {
        static ConstructorInfo result;

        Because of = () => result = typeof(SomethingWithParameterfulConstructors).greediest_constructor();

        It should_return_the_constructor_that_takes_the_most_arguments =() => 
            result.GetParameters().Count().ShouldEqual(2);
    }

    [Subject(typeof(TypeExtensions))]
    public class when_a_generic_type_is_told_to_return_its_proper_name 
    {
        static string result;

        Because of = () => result = typeof(List<int>).proper_name();

        It should_return_a_name_that_has_its_generic_type_arguments_expanded = () => 
            result.ShouldEqual("List`1<System.Int32>");
    }
    
    public class SomethingWithParameterfulConstructors
    {
        public SomethingWithParameterfulConstructors(IDbConnection connection) : this(connection, null)
        {
        }

        public SomethingWithParameterfulConstructors(IDbConnection connection, IDbCommand command)
        {
            this.connection = connection;
            this.command = command;
        }

        public IDbConnection connection
        {
            get;
            set;
        }

        public IDbCommand command
        {
            get;
            set;
        }
    }
}