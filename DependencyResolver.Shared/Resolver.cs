using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyResolver
{
    /// <summary>
    /// Provides dependency resolving functions
    /// </summary>
    public static class Resolver
    {
        private static IEnumerable<Type> _implementators;

        /// <summary>
        /// Returns instance of implementator of platform specific interface
        /// </summary>
        /// <typeparam name="TService">Type of service for which implementator should be fined.</typeparam>
        /// <returns>Implementator instance</returns>
        public static TService Get<TService>()
        {
            if (_implementators == null)
                _implementators = GetImplementators();

            if (_implementators == null)
                throw new ArgumentNullException($"Can't find implementator type for service {typeof(TService)}");

            foreach (var type in _implementators)
            {
                var implementatorOfTService = type.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(inter => inter == typeof(TService));

                if (implementatorOfTService == null) continue;

                var instance = (TService)Activator.CreateInstance(type);

                return instance;
            }

            throw new ArgumentNullException($"Can't find implementator type for service {typeof(TService)}");
        }

        private static IEnumerable<Type> GetImplementators()
        {
            AppDomain domain = AppDomain.CurrentDomain;

            var implementators = domain.GetAssemblies().SelectMany(ass => ass.GetCustomAttributes<DependencyAttribute>()).Select(attr => attr.ImplementatorType);

            return implementators;
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
