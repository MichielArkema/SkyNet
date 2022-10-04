using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyNet.Bot.Commands
{
    public sealed class CommandsManager
    {
        private readonly Application _application;
        private readonly List<Command> _commands = new List<Command>();
        
        public CommandsManager(Application application)
        {
            _application = application;
        }
        
        public void RegisterCommand(Command command)
        {
            if (this.HasCommand(command))
            {
                return;
            }
            
            this._commands.Add(command);
        }

        private bool HasCommand(Command command)
        {
            return this._commands.Any(cmd =>
                cmd.Name.Equals(command.Name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}