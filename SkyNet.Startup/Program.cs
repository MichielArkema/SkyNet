using System;
using System.Threading.Tasks;
using SkyNet.Bot;

namespace SkyNet.Startup
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            //Put your own Bot Token String here.
            Application app = new Application(resources.token); 
            await app.StartAsync();
            Console.ReadKey();
        }
    }
}