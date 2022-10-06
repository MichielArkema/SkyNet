using System.Collections.Generic;

namespace SkyNet.Bot.Features.ChatFilter
{
    public class ChatFilterConfig
    {
        public List<string> BadWords { get; set; }

        public ChatFilterConfig()
        {
            this.BadWords = new List<string>
            {
                "fuck",
                "bitch",
                "shit"
            };
        }
    }
}