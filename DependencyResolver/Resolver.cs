using System;

namespace DependencyResolver
{
    public static class Resolver
    {
        public static TService Get<TService>()
        {
            return default(TService);
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    public class DependencyAttribute : Attribute
    {
        public Type ImplementatorType { get; }
        public DependencyAttribute(Type implementatorType)
        {
            ImplementatorType = implementatorType;
        }
    }
}
