using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Gaming.PlayFab.CommandLine.Extensions
{
    public interface ICommandFactory
    {
        string Name { get; }

        Version Version { get; }

        ICommand[] GetCommands();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ICommand
    {
        string Id { get; }
        string Description { get; }
        string Name { get; }
        string[] Aliases { get; }
        IEnumerable<ICommandOption> Options { get; }

        MethodInfo GetHandler();
    }

    public interface ICommandOption
    {
        string Name { get; }
        string Description { get; }
        string[] Aliases { get; }
        Type OptionType { get; }

        T GetValue<T>();
    }
}
