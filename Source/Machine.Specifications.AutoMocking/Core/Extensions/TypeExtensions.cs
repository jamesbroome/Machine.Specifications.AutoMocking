namespace Machine.Specifications.AutoMocking.Core.Extensions
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Utility;

    #endregion

    public static class TypeExtensions
    {
        public const string generic_argument_type_format = "<{0}>";

        public static ConstructorInfo greediest_constructor(this Type type)
        {
            return type.GetConstructors().OrderByDescending(x => x.GetParameters().Count()).First();
        }

        public static string proper_name(this Type type)
        {
            var message = new StringBuilder(type.Name);
            if (!type.IsGenericType) return message.ToString();

            type.GetGenericArguments().Each(x => message.AppendFormat(generic_argument_type_format, x));

            return message.ToString();
        }
    }
}