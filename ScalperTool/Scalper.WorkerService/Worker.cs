using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Scalper.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                CheckStockElgiganten();
                CheckStockMediaMarkt();
                CheckStockNetonnet();
                await Task.Delay(60000, stoppingToken);
            }
        }

        private async void CheckStockElgiganten() 
        {
            var result = await _httpClient.GetAsync("https://localhost:5001/api/elgiganten/playstationfive/stock");

            LogResult(result, "Elgiganten");
        }

        private async void CheckStockMediaMarkt()
        {
            var result = await _httpClient.GetAsync("https://localhost:5001/api/mediamarkt/playstationfive/stock");

            LogResult(result, "MediaMarkt");
        }

        private async void CheckStockNetonnet()
        {
            var result = await _httpClient.GetAsync("https://localhost:5001/api/netonnet/playstationfive/stock");

            LogResult(result, "Netonnet");
        }

        private async void LogResult(HttpResponseMessage result, string website)
        {
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                _logger.LogInformation($"{DateTime.Now} | Order can be placed at {website}: {content}");
            }
            else
            {
                _logger.LogError($"{DateTime.Now} | Hmm..something went wrong at {website}!");
            }
        }
    }
}
