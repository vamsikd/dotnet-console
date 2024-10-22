using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


Console.WriteLine(string.Join(", ", args));

var appConfig = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .AddCommandLine(args)
    .Build();

    var host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration((context, config) =>
        {
           config.Sources.Clear();
           config.AddConfiguration(appConfig);
        })
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<HostedService>();        
        })
        .Build();
    
    await host.RunAsync();




