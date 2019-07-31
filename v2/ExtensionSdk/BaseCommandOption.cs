using System;

namespace Microsoft.Gaming.PlayFab.CommandLine.Extensions
{
    internal class BaseCommandOption : ICommandOption
    {
        private object _value;

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string[] Aliases { get; set; }

        public Type OptionType { get; set; }

        public CommandOptionType Style { get; set; }

        public bool IsRequired { get; set; }

        public Func<object, (bool IsValid, string ErrorMessage)> Validator { get; set; }

        public string BaseName
        {
            get
            {
                if (Name != null)
                {
                    return Name.Replace("-", "");
                }

                return Name;
            }
        }

        public T GetValue<T>()
        {
            return (T)Convert.ChangeType(_value, OptionType);
        }
    }
}