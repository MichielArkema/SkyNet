using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using SkyNet.Bot.Config;

namespace SkyNet.Bot.Features.ChatFilter
{
    public class ChatFilter : BotFeature
    {
        public ChatFilterConfig Config = new ChatFilterConfig();

        private readonly ulong[] _whitelist = new ulong[]
        {
            979682143062097931
        };
        
        public ChatFilter(Application application) : base(application)
        {
            this.Application.DiscordClient.Connected += this.OnConnected;
            this.Application.DiscordClient.MessageReceived += this.OnMessageReceived;
        }

        private Task OnConnected()
        {
            JsonConfig parser = new JsonConfig("chat-filter.json");

            if (parser.HasConfig)
            {
                this.Config = parser.LoadConfig<ChatFilterConfig>();
            }
            else
            {
                parser.SaveConfig(this.Config);
            }
            
            this.Application.CommandsManager.RegisterCommand(new BadWordsCommand(this.Application, this));
            
            Console.WriteLine("[SkyNet.ChatFilter] Chat Filter has been initialized.");
            return Task.CompletedTask;
        }

        private async Task OnMessageReceived(SocketMessage socket)
        {
            SocketUser user = socket.Author;

            if (user.IsBot || user.IsWebhook)
            {
                return;
            }

            if (this._whitelist.Contains(user.Id))
                return;
            
            string content = socket.Content;
            foreach (string word in this.Config.BadWords)
            {
                if (content.ToLower().Contains(word))
                {
                    await socket.DeleteAsync();
                    await socket.Channel.SendMessageAsync("Please use appropriate language in the chat " + user.Mention);
                    break;
                }
            }
        }
    }
}