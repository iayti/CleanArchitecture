using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Client.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // You must run WebApi and Client.WorkerService together.
                    Console.WriteLine("Hello World!");
                    string baseUrl = "https://localhost:5001";
                    LoginClient loginClient = new LoginClient(baseUrl);
                    var result = await loginClient.CreateAsync(new GetTokenQuery
                    {
                        Email = "test@test.com",
                        Password = "Matech_1850"
                    }, stoppingToken);

                    if (result.Succeeded)
                    {
                        CitiesClient citiesClient = new CitiesClient(baseUrl);
                        citiesClient.SetBearerToken(result.Data.Token);
                        var res = await citiesClient.GetAllCitiesAsync(stoppingToken);     //consume a webApi get action
                        foreach (var item in res.Data)
                        {
                            Console.WriteLine($"City: { item.Name} ");
                            foreach (var dist in item.Districts)
                            {
                                Console.WriteLine($"District: { dist.Name} ");
                            }
                        }
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Error Message: ", ex);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
