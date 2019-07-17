namespace Microsoft.Gaming.PlayFab.CommandLine.MultiplayerServers
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Reflection;
    using Microsoft.Gaming.PlayFab.CommandLine.Extensions;

    [Export(typeof(ICommandFactory))]
    public class CommandFactory : ICommandFactory
    {
        public string Name => "Multiplayer Servers";
        public Version Version => GetType().Assembly.GetName().Version;

        public ICommand[] GetCommands()
        {
            return new ICommand[]
            {
                // new BasicCommand
                // {
                //     Name = "builds",
                //     Id = "com.microsoft.gaming.playfab.multiplayerservers.builds",
                //     Description = "Add, update, or delete builds.",
                //     Aliases = new string[]
                //     {
                //         "b"
                //     },
                //     Options = new List<ICommandOption>()
                //     {

                //     }
                // }
            };
        }
    }
}
