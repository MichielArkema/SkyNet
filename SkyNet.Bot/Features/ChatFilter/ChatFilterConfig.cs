namespace SkyNet.Bot.Features.ChatFilter
{
    public class ChatFilterConfig
    {
        public string[] BadWords { get; set; }

        public ChatFilterConfig()
        {
            this.BadWords = new string[]
            {
                "fuck",
                "bitch",
                "shit"
            };
        }
    }
}