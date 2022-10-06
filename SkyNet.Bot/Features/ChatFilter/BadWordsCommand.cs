using System.Linq;
using Discord.WebSocket;
using SkyNet.Bot.Commands;

namespace SkyNet.Bot.Features.ChatFilter
{
    public class BadWordsCommand : Command
    {
        private readonly ChatFilter _chatFilter;
        
        public BadWordsCommand(Application application, ChatFilter chatFilter) : base(application)
        {
            this._chatFilter = chatFilter;
        }

        public override string Name => "badwords";
        public override string Summary => "Manage the list of bad words";
        public override string Description => "Manage the list of bad words using either the add, remove or check command.";
        public override string Syntax => "Usage: !badwords <add, remove, check> <word>";
        public override void Execute(CommandExecutionContext context)
        {
            if (context.Args.Length == 0)
            {
                this.ShowHelp(context);
                return;
            }

            switch (context.Args[0])
            {
                case "add":
                    this.AddWord(context);
                    break;
                case "remove":
                    this.RemoveWord(context);
                    break;
                case "check":
                    this.CheckWord(context);
                    break;
            }
        }

        private void AddWord(CommandExecutionContext context)
        {
            ISocketMessageChannel channel = context.OriginalMessage.Channel;
            SocketUser user = context.User;
            
            if (context.Args.Length != 2)
            {
                channel.SendMessageAsync(user.Mention + " Syntax: !badwords add <word>");
                return;
            }

            string badWord = context.Args[1];
            if (this._chatFilter.Config.BadWords.Contains(badWord))
            {
                channel.SendMessageAsync(user.Mention + $" The Word {badWord} already is blocked.");
                return;
            }

            this._chatFilter.Config.BadWords.Add(badWord);
            channel.SendMessageAsync(user.Mention + $" The word \"{badWord}\" is now blocked.");
        }

        private void RemoveWord(CommandExecutionContext context)
        {
            ISocketMessageChannel channel = context.OriginalMessage.Channel;
            SocketUser user = context.User;
            
            if (context.Args.Length != 2)
            {
                channel.SendMessageAsync(user.Mention + " Syntax: !badwords remove <word>");
                return;
            }

            string badWord = context.Args[1];
            if (!this._chatFilter.Config.BadWords.Contains(badWord))
            {
                channel.SendMessageAsync(user.Mention + $" The Word {badWord} is not blocked");
                return;
            }

            this._chatFilter.Config.BadWords.Remove(badWord);
            channel.SendMessageAsync(user.Mention + $" The word \"{badWord}\" is now been unblocked.");
        }
        
        private void CheckWord(CommandExecutionContext context)
        {
            ISocketMessageChannel channel = context.OriginalMessage.Channel;
            SocketUser user = context.User;
            
            if (context.Args.Length != 2)
            {
                channel.SendMessageAsync(user.Mention + " Syntax: !badwords check <word>");
                return;
            }

            string badWord = context.Args[1];
            if (!this._chatFilter.Config.BadWords.Contains(badWord))
            {
                channel.SendMessageAsync(user.Mention + $" The Word {badWord} is not blocked!");
                return;
            }
            
            channel.SendMessageAsync(user.Mention + $" The word \"{badWord}\" is blocked!");
        }
        
        private void ShowHelp(CommandExecutionContext context)
        {
            context.OriginalMessage.Channel.SendMessageAsync(context.User.Mention + $"```{string.Join("\n", this.GetHelpMessages())}```");
        }

        private string[] GetHelpMessages()
        {
            return new string[]
            {
                "Syntax: !badwords add <word>",
                "Syntax: !badwords remove <word>",
                "Syntax: !badwords check <word>"
            };
        }
    }
}