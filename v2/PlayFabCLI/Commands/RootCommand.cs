using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Gaming.PlayFab.CommandLine.Extensions;

namespace Microsoft.Gaming.PlayFab.CommandLine
{
    internal class RootCommand : BaseCommand
    {
        public RootCommand(ICommandFactory factory) : base(factory) { }

        public override string Name => "";

        public override string Id => "com.microsoft.gaming.playfab.core.root";

        public override string Description => @"
______ _            ______    _       _____  _     _____ 
| ___ \ |           |  ___|  | |     /  __ \| |   |_   _|
| |_/ / | __ _ _   _| |_ __ _| |__   | /  \/| |     | |  
|  __/| |/ _` | | | |  _/ _` | '_ \  | |    | |     | |  
| |   | | (_| | |_| | || (_| | |_) | | \__/\| |_____| |_ 
\_|   |_|\__,_|\__, \_| \__,_|_.__/   \____/\_____/\___/ 
                __/ |                                    
               |___/                                     
";

        public override string[] Aliases => new string[] { };

        public override IEnumerable<ICommandOption> Options => new List<ICommandOption>
        {
        };

        public override int Invoke(IDictionary<string, object> parsedArguments)
        {
            return 0;
        }
    }
}