using System;

namespace SkyNet.Bot.Commands
{
    public sealed class CommandDoesNotExistException : Exception
    {
        public CommandDoesNotExistException(string message) : base(message) { }
    }
}