namespace Microsoft.Gaming.PlayFab.CommandLine
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;
    using System.CommandLine.Builder;
    using System.CommandLine.Invocation;
    using System.Composition;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Gaming.PlayFab.CommandLine.Extensions;

    class Program
    {
        static async Task<int> Main(string[] args)
        {
            ExtensionManager extMgr = new ExtensionManager();
            IEnumerable<ICommandFactory> extensions = await extMgr.DiscoverAsync("..\\extensions");

            Console.WriteLine("Command factories discovered:");
            foreach (var extension in extensions)
            {
                Console.WriteLine($"{extension.Name} ({extension.Version})");
                foreach (var command in extension.GetCommands())
                {
                    Console.Write("+--> ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(command.Name);
                    Console.ResetColor();
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            ICommandBuilder builder = new CommandBuilder();
            IEnumerable<Command> discoveredCommands = await builder.BuildAsync(extensions.ToArray());

            RootCommand root = new RootCommand
            {
                Description = "Provides automated administration of titles, players, assets, and other PlayFab resources."
            };

            foreach (Command cmd in discoveredCommands)
            {
                root.AddCommand(cmd);
            }

            // var commandLine = new CommandLineBuilder(root)
            //     .UseHelp()
            //     .UseSuggestDirective()
            //     .UseExceptionHandler()
            //     .RegisterWithDotnetSuggest()
            //     .Build();

            return await root.InvokeAsync(args);
        }
    }
}
