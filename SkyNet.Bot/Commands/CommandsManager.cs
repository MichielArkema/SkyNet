using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace SkyNet.Bot.Commands
{
    public sealed class CommandsManager
    {
        private readonly Application _application;
        private readonly List<Command> _commands = new List<Command>();
        private readonly CommandParser _parser = new CommandParser();
            
        public IReadOnlyList<Command> Commands => this._commands;

        public CommandsManager(Application application)
        {
            _application = application;
            this._application.DiscordClient.MessageReceived += this.OnMessageReceived;
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
        
        private Task OnMessageReceived(SocketMessage socket)
        {
            try
            {
                if (socket.Author.IsBot || socket.Author.IsWebhook)
                    return Task.CompletedTask;

                SocketGuildUser user = socket.Author as SocketGuildUser;
                if(user == null)
                    return Task.CompletedTask;
                
                ISocketMessageChannel channel = socket.Channel;
                string content = socket.Content;

                if (!content.StartsWith("!"))
                    return Task.CompletedTask;
            
                Tuple<string, string[]> parsedCommand = this._parser.ParseCommand(content, '!');
                Command command = this._commands.Find(cmd =>
                    cmd.Name.Equals(parsedCommand.Item1, StringComparison.InvariantCulture));

                if (command.Roles.Length != 0)
                {
                    bool hasRequiredRole = command.Roles.Any(role => user.Roles.Any(urole => urole.Id == role));
                    if (!hasRequiredRole)
                    {
                        channel.SendMessageAsync(user.Mention + " You don't have access to this command!");
                        return Task.CompletedTask;
                    }
                }

                if (command.AllowedChannels.Length != 0)
                {
                    bool inAllowedChannel = command.AllowedChannels.Contains(channel.Id);
                    if (!inAllowedChannel)
                    {
                        channel.SendMessageAsync(user.Mention + " This command cannot be executed inside this channel!");
                        return Task.CompletedTask;
                    }
                    
                }
                
                command?.Execute(new CommandExecutionContext(user, parsedCommand.Item2, socket));
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception has thrown while trying to detect for a chat command. Error: {e}");
                return Task.CompletedTask;
            }
        }
    }
}