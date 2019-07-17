using System;

namespace Microsoft.Gaming.PlayFab.CommandLine.Extensions
{
    internal abstract class BaseCommandFactory : ICommandFactory
    {
        public virtual string Name { get; internal set; }

        public virtual Version Version => GetType().Assembly.GetName().Version;

        public abstract ICommand[] GetCommands();
    }
}