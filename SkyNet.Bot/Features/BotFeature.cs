namespace SkyNet.Bot.Features
{
    public abstract class BotFeature
    {
        protected readonly Application Application;

        protected BotFeature(Application application)
        {
            this.Application = application;
        }
    }
}