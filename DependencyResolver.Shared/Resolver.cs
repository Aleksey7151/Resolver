using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyResolver
{
    public static class Resolver
    {
        private static IEnumerable<Type> _implementators;
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
