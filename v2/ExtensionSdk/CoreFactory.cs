namespace Microsoft.Gaming.PlayFab.CommandLine.Extensions
{
    using System;
    using System.Composition;

    [Export(typeof(ICommandFactory))]
    internal class CoreFactory : BaseCommandFactory
    {
        public CoreFactory() { }

        public override string Name => "Core";

        public override ICommand[] GetCommands()
        {
            return new ICommand[]
            {
                new LoginCommand()
            };
        }
    }
}