using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SendGrid;

namespace WorkerService_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config => config.AddUserSecrets(Assembly.GetExecutingAssembly()))
                .ConfigureServices((hostContext, services) =>
                {
                    var sendGridApiKey = hostContext.Configuration.GetSection("SendGrid:ApiKey").Value;

                    if (string.IsNullOrWhiteSpace(sendGridApiKey))
                    {
                        throw new InvalidOperationException("Can not start the service without a valid SendGrid API key!");
                    }

                    services.AddScoped<ISendGridClient>(s => new SendGridClient(new SendGridClientOptions
                    {
                        ApiKey = sendGridApiKey
                    }));

                    services.AddHostedService<Worker>();
                });
    }
}
