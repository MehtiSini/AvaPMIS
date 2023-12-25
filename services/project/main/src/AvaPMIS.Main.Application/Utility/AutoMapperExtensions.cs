using System.Reflection;
using AutoMapper;

namespace AvaPMIS.Main.Utility
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                var sourcePropert= sourceType.GetProperty(property.Name,flags);
                if (sourcePropert==null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;

        }
    }
}