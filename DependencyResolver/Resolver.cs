using System;

namespace DependencyResolver
{
    /// <summary>
    /// Provides dependency resolving functions
    /// </summary>
    public static class Resolver
    {
        /// <summary>
        /// Returns instance of implementator of platform specific interface
        /// </summary>
        /// <typeparam name="TService">Type of service for which implementator should be fined.</typeparam>
        /// <returns>Implementator instance</returns>
        public static TService Get<TService>()
        {
            return default(TService);
        }
    }

    /// <summary>
    /// Attribute for determining the Type of instance, who emplements specific interface
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class DependencyAttribute : Attribute
    {
        /// <summary>
        /// Type of implementator of specific interface
        /// </summary>
        public Type ImplementatorType { get; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="implementatorType">type of implementator</param>
        public DependencyAttribute(Type implementatorType)
        {
            ImplementatorType = implementatorType;
        }
    }
}
