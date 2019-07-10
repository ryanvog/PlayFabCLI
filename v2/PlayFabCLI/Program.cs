namespace Microsoft.Gaming.PlayFab.CommandLine
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Threading.Tasks;
    using Microsoft.Gaming.PlayFab.CommandLine.Extensions;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World Too!");

            ExtensionManager extMgr = new ExtensionManager();
            var extensions = await extMgr.DiscoverAsync("..\\extensions");

            Console.WriteLine("Command factories discovered:");
            foreach (var extension in extensions)
            {
                foreach (var command in extension.GetCommands())
                {
                    Console.WriteLine(command.Name);
                }
            }

        }
    }
}
