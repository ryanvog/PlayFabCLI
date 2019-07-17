using System;

namespace Microsoft.Gaming.PlayFab.CommandLine.Extensions
{
    internal class BaseCommandOption : ICommandOption
    {
        private object _value;

        public string Name { get; set; }

        public string Description { get; set; }

        public string[] Aliases { get; set; }

        public Type OptionType { get; set; }

        public T GetValue<T>()
        {
            return (T)Convert.ChangeType(_value, OptionType);
        }
    }
}