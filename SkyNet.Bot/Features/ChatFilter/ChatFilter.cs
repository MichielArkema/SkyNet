using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using SkyNet.Bot.Config;

namespace SkyNet.Bot.Features.ChatFilter
{
    public class ChatFilter : BotFeature
    {
        private ChatFilterConfig _config = new ChatFilterConfig();
        
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
                this._config = parser.LoadConfig<ChatFilterConfig>();
            }
            else
            {
                parser.SaveConfig(this._config);
            }
            
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

            string content = socket.Content;
            foreach (string word in this._config.BadWords)
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