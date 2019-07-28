using System;
using System.Composition;
using Microsoft.Gaming.PlayFab.CommandLine.Extensions;

namespace Microsoft.Gaming.PlayFab.CommandLine
{
    [Export(typeof(ICommandFactory))]
    internal class CoreFactory : BaseCommandFactory
    {
        public CoreFactory() { }

        public override string Name => "Core";

        public override ICommand[] GetCommands()
        {
            return new ICommand[]
            {
                new LoginCommand(this),
                new RootCommand(this),
            };
        }
    }
}