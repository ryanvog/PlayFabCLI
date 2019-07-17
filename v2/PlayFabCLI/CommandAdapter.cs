namespace Microsoft.Gaming.PlayFab.CommandLine
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Threading.Tasks;
    using Cli = Microsoft.Gaming.PlayFab.CommandLine.Extensions;

    internal interface ICommandAdapter
    {
        Task<Command> AdaptAsync(Cli.ICommand command);
    }

    internal class CommandAdapter : ICommandAdapter
    {
        public CommandAdapter() { }

        public async Task<Command> AdaptAsync(Cli.ICommand command)
        {
            IOptionAdapter optionAdapter = new OptionAdapter();

            var newCommand = new Command(
                    command.Name,
                    command.Description)
            {
                Handler = CommandHandler.Create(command.GetHandler()),
                TreatUnmatchedTokensAsErrors = true,
                
            };

            foreach (Cli.ICommandOption option in command.Options)
            {
                newCommand.AddOption(await optionAdapter.AdaptAsync(option));
            }

            return newCommand;
        }
    }
}