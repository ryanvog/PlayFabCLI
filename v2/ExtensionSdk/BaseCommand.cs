using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Microsoft.Gaming.PlayFab.CommandLine.Extensions
{
    internal abstract class BaseCommand : ICommand
    {
        public BaseCommand(ICommandFactory factory)
        {
            Factory = factory;
        }

        public abstract string Name { get; }

        public abstract string Id { get; }

        public abstract string Description { get; }

        public abstract string[] Aliases { get; }

        public abstract IEnumerable<ICommandOption> Options { get; }

        public ICommandFactory Factory { get; }

        public abstract int Invoke(IDictionary<string, object> parsedArguments);
    }
}