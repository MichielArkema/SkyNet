using System;

namespace SkyNet.Bot.Commands
{
    public sealed class CommandParser
    {
        public Tuple<string, string[]> ParseCommand(string content, char commandModifier)
        {
            string[] splittedContent = content.Split(' ');

            string commandName = splittedContent[0].Trim(commandModifier);
            string[] args = { };

            if (splittedContent.Length > 1)
            {
                splittedContent.CopyTo(args, 1);
            }
            
            return new Tuple<string, string[]>(commandName, args);
        }
    }
}