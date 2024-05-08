using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop_Api.AppDbContext;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop_Api.Services
{
    public class PromotionUpdateService : BackgroundService
    {
        private readonly IServiceProvider _provider;

        public PromotionUpdateService(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _provider.CreateScope())
                {
                    var productService = scope.ServiceProvider.GetRequiredService<ProductService>();
                    productService.UpdatePromotionStatusAndProductPrice();
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
