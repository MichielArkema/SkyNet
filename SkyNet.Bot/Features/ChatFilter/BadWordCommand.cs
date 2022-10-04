using Discord.WebSocket;
using SkyNet.Bot.Commands;

namespace SkyNet.Bot.Features.ChatFilter
{
    public sealed class BadWordCommand : Command
    {
        public BadWordCommand(Application application) : base(application) { }

        public override string Name => "badwords";
        public override string Summary { get; }
        public override string Description { get; }
        public override string Syntax { get; }
        
        public override void Execute(CommandExecutionContext context)
        {
            if (context.Args.Length == 0)
            {
                this.ShowHelp(context);
                return;
            }
        }

        private async void ShowHelp(CommandExecutionContext context)
        {
            ISocketMessageChannel channel = context.OriginalMessage.Channel;

            await channel.SendMessageAsync($"```{string.Join("\n", this.GetHelpTextMessages())}```");
        }

        private string[] GetHelpTextMessages()
        {
            return new string[]
            {
                "Syntax: /badwords add <word>",
                "Syntax: /badwords remove <word>",
                "Syntax: /badwords check <word>"
            };
        }
    }
}