using System;
using System.Composition;
using Microsoft.Gaming.PlayFab.CommandLine.Extensions;

namespace Microsoft.Gaming.PlayFab.CommandLine.MultiplayerServers
{
    [Export(typeof(ICommandFactory))]
    public class CommandFactory : ICommandFactory
    {
        public ICommand[] GetCommands()
        {
            return new ICommand[] 
            {
                new CommandProxy
                {
                    Name = "MultiplayerServers",
                }
            };
        }
    }
}
