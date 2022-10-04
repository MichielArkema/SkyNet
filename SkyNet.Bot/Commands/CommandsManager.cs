using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyNet.Bot.Commands
{
    public sealed class CommandsManager
    {
        private readonly Application _application;
        private readonly List<Command> _commands = new List<Command>();
        
        public IReadOnlyList<Command> Commands => this._commands;

        public CommandsManager(Application application)
        {
            _application = application;
        }
        
        public void RegisterCommand(Command command)
        {
            if (this.HasCommand(command))
            {
                throw new CommandAlreadyExistsException($"The command \"${command.Name}\" is already registered!");
            }
            
            this._commands.Add(command);
        }

        public void RemoveCommand(Command command)
        {
            if (!this.HasCommand(command))
            {
                throw new CommandDoesNotExistException($"The command \"{command.Name}\" does not registered!");
            }

            this._commands.Remove(command);
        }

        public bool HasCommand(Command command)
        {
            return this._commands.Any(cmd =>
                cmd.Name.Equals(command.Name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}