using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Gaming.PlayFab.CommandLine.Extensions;

namespace Microsoft.Gaming.PlayFab.CommandLine
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            ExtensionManager extMgr = new ExtensionManager();
            string pluginPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Extensions");
            IEnumerable<ICommandFactory> extensions = await extMgr.DiscoverAsync(pluginPath);

            ICommandBuilder builder = new CommandBuilder();
            CommandLineApplication rootCommand = await builder.BuildSubCommandsAsync(extensions.ToArray());

            return (rootCommand.Execute(args));
        }

    }
}
