using System;

namespace SkyNet.Bot.Commands
{
    public sealed class CommandAlreadyExistsException : Exception
    {
        public CommandAlreadyExistsException(string message) : base(message) { }
    }
}