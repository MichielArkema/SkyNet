using System;
using Discord;
using Discord.WebSocket;

namespace SkyNet.Bot.Commands
{
    public abstract class Command
    {
        protected readonly Application _application;

        protected Command(Application application)
        {
            _application = application;
        }

        public abstract string Name { get; }

        public abstract string Summary { get; }

        public abstract string Description { get; }

        public abstract string Syntax { get; }

        public virtual ulong[] AllowedChannels => Array.Empty<ulong>();

        public virtual ulong[] Roles => Array.Empty<ulong>();
        
        public abstract void Execute(CommandExecutionContext context);
    }
}