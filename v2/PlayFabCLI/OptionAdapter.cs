namespace Microsoft.Gaming.PlayFab.CommandLine
{
    using System.CommandLine;
    using System.Threading.Tasks;
    using Cli = Microsoft.Gaming.PlayFab.CommandLine.Extensions;

    internal interface IOptionAdapter
    {
        Task<Option> AdaptAsync(Cli.ICommandOption option);
    }

    internal class OptionAdapter : IOptionAdapter
    {
        public Task<Option> AdaptAsync(Cli.ICommandOption option)
        {
            Option newOption = new Option(option.Name, option.Description);

            foreach (string alias in option.Aliases)
            {
                newOption.AddAlias(alias);
            }

            newOption.Argument = new Argument
            {
                ArgumentType = option.OptionType,
                Arity = ArgumentArity.ExactlyOne,
            };

            return Task.FromResult(newOption);
        }
    }

}