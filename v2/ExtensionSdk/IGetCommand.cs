using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

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
        ICommandFactory Factory { get; }

        int Invoke(IDictionary<string, object> parsedArguments);
    }

    public interface ICommandOption
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        string[] Aliases { get; }
        Type OptionType { get; }

        string BaseName { get; }
        T GetValue<T>();
    }
}
