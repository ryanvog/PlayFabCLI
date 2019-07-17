namespace Microsoft.Gaming.PlayFab.CommandLine
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Composition.Hosting;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Loader;
    using System.Threading.Tasks;
    using Microsoft.Gaming.PlayFab.CommandLine.Extensions;

    /// <summary>
    /// 
    /// </summary>
    public class ExtensionManager
    {
        /// <summary>
        /// Gets discovered extensions
        /// </summary>
        /// <value></value>
        [ImportMany]
        public IEnumerable<ICommandFactory> Extensions { get; private set; }

        /// <summary>
        /// 
        /// <param name="pluginFolders"></param>
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ICommandFactory>> DiscoverAsync(params string[] pluginFolders)
        {
            await ComposeAsync(pluginFolders.FirstOrDefault());
            return Extensions;
        }

        private Task ComposeAsync(string pluginFolder)
        {
            if (!Directory.Exists(pluginFolder))
            {
                Extensions = new ICommandFactory[] { };
                return Task.CompletedTask;
            }

            // Catalogs does not exists in Dotnet Core, so you need to manage your own.
            var assemblies = new List<Assembly>() { typeof(Program).GetTypeInfo().Assembly };
            var pluginAssemblies = Directory.GetFiles(
                Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Extensions"), "*.dll", SearchOption.TopDirectoryOnly)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                // Ensure that the assembly contains an implementation for the given type.
                .Where(s => s.GetTypes().Where(p => typeof(ICommandFactory).IsAssignableFrom(p)).Any());
            assemblies.AddRange(pluginAssemblies);
            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);
            using (var container = configuration.CreateContainer())
            {
                Extensions = container.GetExports<ICommandFactory>();
            }

            return Task.CompletedTask;
        }
    }
}