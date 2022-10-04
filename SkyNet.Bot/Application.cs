using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using SkyNet.Bot.Commands;
using SkyNet.Bot.Features.ChatFilter;

namespace SkyNet.Bot
{
    public class Application
    {
        private readonly string _token;
        
        public CommandsManager CommandsManager { get; private set; }
        public DiscordSocketClient DiscordClient { get; private set; }

        public ChatFilter ChatFilter { get; private set; }
        
        public async Task StartAsync()
        {
            try
            {
                if (this.DiscordClient != null)
                    return;

                DiscordSocketConfig config = new DiscordSocketConfig
                {
                    GatewayIntents = GatewayIntents.All
                };
            
                this.DiscordClient = new DiscordSocketClient(config);
                this.DiscordClient.Log += this.OnLog;

                this.CommandsManager = new CommandsManager(this);
                
                this.ChatFilter = new ChatFilter(this);
            
                await this.DiscordClient.LoginAsync(TokenType.Bot, this._token);
                await this.DiscordClient.StartAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        private Task OnLog(LogMessage arg)
        {
            Console.WriteLine(arg.Message);
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
        
        public Application(string token)
        {
            _token = token;
        }
    }
}