using System;

namespace Microsoft.Gaming.PlayFab.CommandLine
{
    internal class DuplicateKeyException : Exception
    {
        public DuplicateKeyException() { }

        public DuplicateKeyException(string key) : base($"The key '{key}' has already been registered.") { }
    }
}