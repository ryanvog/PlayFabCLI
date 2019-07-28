namespace Microsoft.Gaming.PlayFab.CommandLine
{
    using System.Linq;
    using System.Threading.Tasks;
    using McMaster.Extensions.CommandLineUtils;
    using Cli = Microsoft.Gaming.PlayFab.CommandLine.Extensions;

    internal interface ICommandOptionAdapter
    {
        Task<CommandOption> AdaptAsync(Cli.ICommandOption option);
    }

    internal class CommandOptionAdapter : ICommandOptionAdapter
    {
        public Task<CommandOption> AdaptAsync(Cli.ICommandOption option)
        {
            var newOption = new CommandOption(
                string.Join('|', option.Aliases.Union(new string[] { option.Name })),
                CommandOptionType.SingleOrNoValue)
            {
                Description = option.Description,
                ShowInHelpText = true,
            };

            return Task.FromResult(newOption);
        }
    }
}