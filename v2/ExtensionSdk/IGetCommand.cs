using System;

namespace Microsoft.Gaming.PlayFab.CommandLine.Extensions
{
    public interface ICommandFactory
    {
        ICommand[] GetCommands();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ICommand
    {
        string Name { get; }
    }
}
