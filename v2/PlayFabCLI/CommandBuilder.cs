using System;
using System.Collections.Generic;
// using System.CommandLine;
using System.Linq;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Gaming.PlayFab.CommandLine.Extensions;

namespace Microsoft.Gaming.PlayFab.CommandLine
{
    internal interface ICommandBuilder
    {
        Task<CommandLineApplication> BuildSubCommandsAsync(ICommandFactory[] factories);
    }

    internal class CommandBuilder : ICommandBuilder
    {
        public async Task<CommandLineApplication> BuildSubCommandsAsync(ICommandFactory[] factories)
        {
            IDictionary<string, ICommand> commandMap = new Dictionary<string, ICommand>();

            var adapter = new CommandLineApplicationAdapter();
            ICommand rootCommand = null;
            string rootKey = new RootCommand(null).Id;

            foreach (var factory in factories)
            {
                foreach (var importedCommand in factory.GetCommands())
                {
                    try
                    {
                        commandMap.Add(importedCommand.Id, importedCommand);
                    }
                    catch (ArgumentException)
                    {
                        throw new DuplicateKeyException(importedCommand.Id);
                    }
                }
            }

            if (!commandMap.TryGetValue(rootKey, out rootCommand))
            {
                throw new Exception("Not root command registered.");
            }

            IEnumerable<ICommand> subCommands =
                commandMap
                    .Where(x => !x.Key.Equals(rootKey, StringComparison.InvariantCultureIgnoreCase))
                    .OrderBy(x => x.Value.Name)
                    .Select(x => x.Value);


            CommandLineApplication root = await adapter.AdaptAsync(rootCommand);

            foreach (ICommand subCommand in subCommands)
            {
                root.Commands.Add(await adapter.AdaptAsync(subCommand));
            }

            return root;
        }
    }
}