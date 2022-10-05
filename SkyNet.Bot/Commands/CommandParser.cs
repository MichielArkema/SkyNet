using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyNet.Bot.Commands
{
    public sealed class CommandParser
    {
        public Tuple<string, string[]> ParseCommand(string content, char commandModifier)
        {
            string[] splittedContent = content.Split(' ');
            string commandName = splittedContent[0].Trim(commandModifier);

            List<string> args = splittedContent.ToList();
            args.RemoveAt(0);
            
            return new Tuple<string, string[]>(commandName, args.ToArray());
        }
    }
}