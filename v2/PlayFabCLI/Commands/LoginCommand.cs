using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Gaming.PlayFab.CommandLine.Extensions;
using PlayFab;
using PlayFab.ClientModels;

namespace Microsoft.Gaming.PlayFab.CommandLine
{
    internal class LoginCommand : BaseCommand
    {
        public LoginCommand(ICommandFactory factory) : base(factory) { }

        public override string Name => "login";

        public override string Id => "com.microsoft.gaming.playfab.core.login";

        public override string Description => "Provides access to authenticated areas of the CLI.";

        public override string[] Aliases => new string[]
        {
            "l"
        };

        public override int Invoke(IDictionary<string, object> parsedArguments)
        {
            Task<PlayFabResult<LoginResult>> t =
                PlayFabClientAPI.LoginWithEmailAddressAsync(
                    new LoginWithEmailAddressRequest
                    {
                        Email = parsedArguments["emailAddress"].ToString(),
                        Password = parsedArguments["password"].ToString(),
                        TitleId = parsedArguments["titleId"].ToString(),
                    });

            t.Wait();

            //  response.Result.EntityToken.EntityToken;
            return 0;
        }

        public override IEnumerable<ICommandOption> Options => new List<ICommandOption>
        {
            new BaseCommandOption
            {
                Id = "com.microsoft.gaming,playfab.core.login.emailaddress",
                Name = "--email-address",
                Aliases = new string[] { "-e" },
                Description = "Email address used to acquire an entity token.",
                OptionType = typeof(string),
            },
            new BaseCommandOption
            {
                Id = "com.microsoft.gaming,playfab.core.login.password",
                Name = "--password",
                Aliases = new string[] { "-p" },
                Description = "Password used to acquire an entity token.",
                OptionType = typeof(string),
            },
            new BaseCommandOption
            {
                Id = "com.microsoft.gaming,playfab.core.login.titleid",
                Name = "--title-id",
                Aliases = new string[] { "-t" },
                Description = "The title ID used to acquire an entity token.",
                OptionType = typeof(string),
            },
        };
    }
}