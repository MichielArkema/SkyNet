using Discord.WebSocket;

namespace SkyNet.Bot.Commands
{
    public sealed class CommandExecutionContext
    {
        public SocketUser User { get; }

        public string[] Args { get; }
        
        public SocketMessage OriginalMessage { get; }
        
        public CommandExecutionContext(SocketUser user, string[] args, SocketMessage originalMessage)
        {
            User = user;
            Args = args;
            OriginalMessage = originalMessage;
        }
    }
}