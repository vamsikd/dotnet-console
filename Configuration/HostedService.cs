
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class HostedService : IHostedService
{
    private readonly IConfiguration _config;
    private readonly ILogger<HostedService> _logger;
    private readonly IHostApplicationLifetime _lifetime;
    
    public HostedService(IConfiguration configuration, IHostApplicationLifetime lifetime, ILogger<HostedService> logger)
    {
        _config = configuration;
        _lifetime = lifetime;
        _logger = logger;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => 
        {            
            try
            {
                _logger.LogInformation("Background service started");     
                var env = _config["Environment"];
               _logger.LogInformation($"Environment: {env}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _lifetime.StopApplication();               
            }
            
            
        });
        
       return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Background service stopped");
        return Task.CompletedTask;
    }
}
