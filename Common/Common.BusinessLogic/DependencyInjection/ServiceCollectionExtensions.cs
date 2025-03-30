using Common.Api.DependencyInjection;
using Common.Api.Execution;
using Common.BusinessLogic.Execution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.BusinessLogic.DependencyInjection
{
    /// <summary>
    /// Dependency injection <see cref="IServiceCollection" /> extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private static readonly Type RegistryBaseType = typeof(RegistryBase);

        /// <summary>
        /// Add all registries and profiles from application assemblies.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static IServiceCollection AddRegistriesAndProfiles(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddRegistriesAndProfiles(IsCandidateLibrary);
        }

        /// <summary>
        /// Add all registries and profiles from application assemblies.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="isCandidateLibrary"></param>
        public static IServiceCollection AddRegistriesAndProfiles(this IServiceCollection serviceCollection, Func<RuntimeLibrary, bool> isCandidateLibrary)
        {
            serviceCollection.TryAddSingleton<IMediator, Mediator>();

            var dependencies = DependencyContext.Default!.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                if (isCandidateLibrary(library))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));

                    AddRegistryAndProfile(serviceCollection, assembly);
                }
            }

            return serviceCollection;
        }

        /// <summary>
        /// Add all registries and profiles from assemblies specified.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="assemblies"></param>
        public static IServiceCollection AddRegistriesAndProfiles(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            serviceCollection.TryAddSingleton<IMediator, Mediator>();

            for (var i = 0; i < assemblies.Length; i++)
            {
                AddRegistryAndProfile(serviceCollection, assemblies[i]);
            }

            return serviceCollection;
        }

        /// <summary>
        /// Need or not process library types.
        /// </summary>
        /// <param name="library"></param>
        public static bool IsCandidateLibrary(RuntimeLibrary library)
        {
            if (library.Name.StartsWith("System.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("Microsoft.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.Equals("AutoMapper", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("AutoMapper.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("MySql.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("BouncyCastle.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("Castle.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.Equals("Dapper", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("Dapper.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("Google.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("Newtonsoft.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.Equals("NLog", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("NLog.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("Swashbuckle.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("K4os.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("Ubiety.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("Zstandard.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("NETStandard.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("runtime.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("SkiaSharp.NativeAssets.", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("librdkafka", StringComparison.OrdinalIgnoreCase) ||
                library.Name.StartsWith("Oracle.ManagedDataAccess", StringComparison.OrdinalIgnoreCase)
            )
            {
                return false;
            }

            return true;
        }

        private static void AddRegistryAndProfile(IServiceCollection serviceCollection, Assembly assembly)
        {
            var registrations = assembly.ExportedTypes
                                        .Where(t => RegistryBaseType == t.BaseType)
                                        .ToArray();

            for (var y = 0; y < registrations.Length; y++)
            {
                Activator.CreateInstance(registrations[y], serviceCollection);
            }

            serviceCollection.AddAutoMapper(assembly);
        }
    }
}
