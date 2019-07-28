using System;
using System.Collections.Generic;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Gaming.PlayFab.CommandLine.Extensions;

namespace Microsoft.Gaming.PlayFab.CommandLine
{
    internal static class CommandLineApplicationExtensions
    {
        internal static IDictionary<string, object> GetParsedArguments(this CommandLineApplication app, ICommand commandRef)
        {
            var results = new Dictionary<string, object>();

            if (app.Arguments.Count > 0)
            {
                var argumentValues = new List<string>();

                foreach (CommandArgument argument in app.Arguments)
                {
                    argumentValues.Add(argument.Value);
                }

                results.Add(commandRef.Id.ToLowerInvariant(), argumentValues);
            }

            foreach (CommandOption option in app.Options)
            {
                ICommandOption sourceOption = GlobalOptions.MandatoryOptions
                    .FirstOrDefault(x =>
                        x.BaseName.Equals(option.LongName, StringComparison.InvariantCultureIgnoreCase)
                        || x.BaseName.Equals(option.ShortName, StringComparison.InvariantCultureIgnoreCase));

                if (sourceOption == null)
                {
                    sourceOption = commandRef.Options
                       .FirstOrDefault(x =>
                           x.BaseName.Equals(option.LongName, StringComparison.InvariantCultureIgnoreCase)
                           || x.BaseName.Equals(option.ShortName, StringComparison.InvariantCultureIgnoreCase));
                }

                if (sourceOption == null)
                {
                    throw new ArgumentException("Extension command option list does not match compiled set.");
                }

                // A switch is indicated as being passed if there is at least one value in the Values collection
                if (option.Values.Count > 0)
                {
                    if (option.Values.Count > 1)
                    {
                        results.Add(sourceOption.Id, option.Values);
                    }
                    else
                    {
                        results.Add(sourceOption.Id, option.Value());
                    }
                }
            }

            return results;
        }
    }
}