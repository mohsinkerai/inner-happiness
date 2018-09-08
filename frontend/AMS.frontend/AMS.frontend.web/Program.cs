using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AMS.frontend.web
{
    public class Program
    {
        #region Public Methods

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        #endregion Public Methods
    }
}