using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()  
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder() //  Build the host. Must implement IWebHost and returns a WebHost object
                .UseKestrel() //  Creates web server and hosts the code. WebHostBuilder requires a server that implements IServer, as this does.
                                
                //  If the app should work with IIS, the UseIISIntegration method should be called as part of building the host. Note that this does not configure a server, like UseKestrel does. 
                //  To use IIS with ASP.NET Core, you must specify both UseKestrel and UseIISIntegration. Kestrel is designed to be run behind a proxy and should not be deployed directly facing the Internet. 
                //  UseIISIntegration specifies IIS as the reverse proxy server.
                .UseIISIntegration() // Reverse proxy using IIS & IIS Express. It does not deal with IServer as Kestrel does. This call configures the port and base path the server should listen on when running behind AspNetCoreModule, and also to capture startup errors. 
                .UseContentRoot(Directory.GetCurrentDirectory()) //  The server’s content root determines where it searches for content files, like MVC View files. The default content root is the folder from which the application is run.
                .UseStartup<Startup>()
                .UseConfiguration(config)
                .Build();

            host.Run();
        }
    }
}
