namespace Microsoft.Gaming.PlayFab.CommandLine.Extensions
{
    using System.Collections.Generic;
    using System.Reflection;

    internal abstract class BaseCommand : ICommand
    {
        public abstract string Name { get; }

        public abstract string Id { get; }

        public abstract string Description { get; }

        public abstract string[] Aliases { get; }

        public abstract IEnumerable<ICommandOption> Options { get; }

        public abstract MethodInfo GetHandler();
    }
}