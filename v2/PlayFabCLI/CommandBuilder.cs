namespace Microsoft.Gaming.PlayFab.CommandLine
{
    using System.Collections.Generic;
    using System.CommandLine;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Gaming.PlayFab.CommandLine.Extensions;

    internal interface ICommandBuilder
    {
        Task<IEnumerable<Command>> BuildAsync(params ICommandFactory[] factories);
    }

    internal class CommandBuilder : ICommandBuilder
    {
        public async Task<IEnumerable<Command>> BuildAsync(params ICommandFactory[] factories)
        {
            IList<Command> results = new List<Command>();
            var adapter = new CommandAdapter();

            foreach (var factory in factories)
            {
                foreach (var importedCommand in factory.GetCommands())
                {
                    results.Add(await adapter.AdaptAsync(importedCommand));
                }
            }

            return results;
        }
    }
}