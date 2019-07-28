using System.Collections.Generic;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Gaming.PlayFab.CommandLine.Extensions;
using Cli = Microsoft.Gaming.PlayFab.CommandLine.Extensions;

namespace Microsoft.Gaming.PlayFab.CommandLine
{
    internal interface ICommandLineApplicationAdapter
    {
        Task<CommandLineApplication> AdaptAsync(Cli.ICommand command);
    }

    internal class CommandLineApplicationAdapter : ICommandLineApplicationAdapter
    {
        public CommandLineApplicationAdapter() { }

        public async Task<CommandLineApplication> AdaptAsync(Cli.ICommand command)
        {
            ICommandOptionAdapter optionAdapter = new CommandOptionAdapter();

            var newCommand = new CommandLineApplication
            {
                Name = command.Name,
                Description = command.Description,
                LongVersionGetter = () => command.Factory.Version.ToString(),
            };

            newCommand.Invoke = () =>
            {
                IDictionary<string, object> parsedArguments = newCommand.GetParsedArguments(command);
                return GlobalOptions.InvokeCommand(parsedArguments, newCommand, command.Invoke);
            };

            // Extension provided options
            foreach (Cli.ICommandOption option in command.Options)
            {
                newCommand.Options.Add(await optionAdapter.AdaptAsync(option));
            }

            // Mandatory options
            foreach (ICommandOption mandatoryOption in GlobalOptions.MandatoryOptions)
            {
                newCommand.Options.Add(await optionAdapter.AdaptAsync(mandatoryOption));
            }

            return newCommand;
        }
    }
}