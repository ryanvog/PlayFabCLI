using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Gaming.PlayFab.CommandLine.Extensions;
using Cli = Microsoft.Gaming.PlayFab.CommandLine.Extensions;

namespace Microsoft.Gaming.PlayFab.CommandLine
{
    internal static class GlobalOptions
    {
        internal static readonly BaseCommandOption HelpOption = new BaseCommandOption
        {
            Id = "com.microsoft.gaming,playfab.core.options.help",
            Name = "--help",
            Aliases = new string[] { "-?" },
            Description = "Provides additional context-sensitive help.",
            Style = Cli.CommandOptionType.NoValue,
        };

        internal static readonly BaseCommandOption VersionOption = new BaseCommandOption
        {
            Id = "com.microsoft.gaming,playfab.core.options.version",
            Name = "--version",
            Aliases = new string[] { },
            Description = "Displays the current version of the CLI.",
            Style = Cli.CommandOptionType.NoValue,
        };

        internal static readonly BaseCommandOption VerboseOption = new BaseCommandOption
        {
            Id = "com.microsoft.gaming,playfab.core.options.verbose",
            Name = "--verbose",
            Aliases = new string[] { "-v" },
            Description = "Displays additional verbose-level logging.",
            Style = Cli.CommandOptionType.NoValue,
        };

        internal static readonly IEnumerable<ICommandOption> MandatoryOptions = new List<ICommandOption>
        {
            HelpOption,
            VersionOption,
            VerboseOption,
        };

        internal static int InvokeCommand(
            IDictionary<string, object> parsedArguments,
            CommandLineApplication command,
            Func<IDictionary<string, object>, int> asyncInvoker)
        {
            if (InvokeGlobalOptions(parsedArguments, command))
            {
                return 1;
            }

            return asyncInvoker(parsedArguments);
        }

        internal static bool InvokeGlobalOptions(
            IDictionary<string, object> parsedArguments,
            CommandLineApplication command)
        {
            object value = null;

            if (parsedArguments.TryGetValue(HelpOption.Id, out value))
            {
                command.ShowHelp();
                return true;
            }

            if (parsedArguments.TryGetValue(VersionOption.Id, out value))
            {
                command.ShowVersion();
                return true;
            }

            if (parsedArguments.TryGetValue(VerboseOption.Id, out value))
            {
                // TODO: set global verbosity setting here.
            }

            return false;
        }
    }
}