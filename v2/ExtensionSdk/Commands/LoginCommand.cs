using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

namespace Microsoft.Gaming.PlayFab.CommandLine.Extensions
{
    internal class LoginCommand : BaseCommand
    {
        public LoginCommand()
        {
        }

        public override string Name => "login";

        public override string Id => "com.microsoft.gaming.playfab.core.login";

        public override string Description => "Provides access to authenticated areas of the CLI.";

        public override string[] Aliases => new string[]
        {
            "l"
        };

        public async Task<string> InvokeCommandAsync(string emailAddress, string password, string titleId)
        {
            PlayFabResult<LoginResult> response =
                await PlayFabClientAPI.LoginWithEmailAddressAsync(
                    new LoginWithEmailAddressRequest
                    {
                        Email = emailAddress,
                        Password = password,
                        TitleId = titleId,
                    });


            return response.Result.EntityToken.EntityToken;
        }

        public override MethodInfo GetHandler()
        {
            return GetType()
                .GetMethods()
                .Where(x => x.Name == nameof(InvokeCommandAsync))
                .First();
        }

        public override IEnumerable<ICommandOption> Options => new List<ICommandOption>
        {
            new BaseCommandOption
            {
                Name = "--email-address",
                Aliases = new string[] { "-e" },
                OptionType = typeof(string),
            },
            new BaseCommandOption
            {
                Name = "--password",
                Aliases = new string[] { "-p" },
                OptionType = typeof(string),
            },
            new BaseCommandOption
            {
                Name = "--title-id",
                Aliases = new string[] { "-t" },
                OptionType = typeof(string),
            },
        };
    }
}