using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CargoPay.Application.Services
{
    public class FeeUpdateService : BackgroundService
    {
        private static readonly Random _random = new();
        private decimal _currentFeeRate = 1.0m;
        private readonly object _lock = new();

        public decimal GetCurrentFeeRate()
        {
            lock (_lock)
            {
                return _currentFeeRate;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                UpdateFeeRate();
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private void UpdateFeeRate()
        {
            lock (_lock)
            {
                var randomDecimal = (decimal)(_random.NextDouble() * 0.2); // Random value between 0.0 and 0.2
                _currentFeeRate = 1.0m + randomDecimal; // Keeps fee in range 1.0 - 1.2
            }
        }
    }
}
