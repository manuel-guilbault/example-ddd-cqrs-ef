using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DddEfSample.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            using (var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build())
            {
                host.Run();
            }
        }
    }
}
