namespace Microsoft.Gaming.PlayFab.CommandLine
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using McMaster.Extensions.CommandLineUtils;
    using Cli = Microsoft.Gaming.PlayFab.CommandLine.Extensions;

    internal interface ICommandOptionAdapter
    {
        Task<CommandOption> AdaptAsync(Cli.ICommandOption option, bool globalOption = false);
    }

    internal class CommandOptionAdapter : ICommandOptionAdapter
    {
        public Task<CommandOption> AdaptAsync(Cli.ICommandOption option, bool globalOption = false)
        {
            CommandOptionType style = AdaptStyle(option.Style);

            var newOption = new CommandOption(
                string.Join('|', option.Aliases.Union(new string[] { option.Name })),
                style)
            {
                Description = option.Description,
                ShowInHelpText = true
            };

            if (option.IsRequired)
            {
                newOption.IsRequired();
            }

            if (globalOption)
            {
                newOption.Inherited = true;
            }

            if (option.Validator != null)
            {
                newOption.Validators.Add(new BaseOptionValidator(newOption, option.Validator));
            }

            return Task.FromResult(newOption);
        }

        private CommandOptionType AdaptStyle(Cli.CommandOptionType style)
        {
            switch (style)
            {
                case Cli.CommandOptionType.SingleValue: return CommandOptionType.SingleValue;
                case Cli.CommandOptionType.SingleOrNoValue: return CommandOptionType.SingleOrNoValue;
                case Cli.CommandOptionType.MultipleValues: return CommandOptionType.MultipleValue;
                case Cli.CommandOptionType.NoValue: return CommandOptionType.NoValue;
                default: return CommandOptionType.SingleValue;
            }
        }
    }
}