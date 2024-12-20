using OkalaCryptoCurrency.Application.Interfaces.ApiClients.CryptoCurrency;
using OkalaCryptoCurrency.Application.Interfaces.Services.CryptoCurrency;
using OkalaCryptoCurrency.Domain.DomainServices.Implementations.CryptoCurrency;
using OkalaCryptoCurrency.Domain.Interfaces.CacheService;
using OkalaCryptoCurrency.Domain.Interfaces.DomainService.CryptoCurrency;
using OkalaCryptoCurrency.Domain.Interfaces.Log;
using OkalaCryptoCurrency.Domain.Interfaces.Repositories.MongoDb;
using OkalaCryptoCurrency.Infrastructure.ApiClients.CryptoCurrency;
using OkalaCryptoCurrency.Infrastructure.CacheService;
using OkalaCryptoCurrency.Infrastructure.Log;
using OkalaCryptoCurrency.Application.Services.Implementations.CryptoCurrency;
using OkalaCryptoCurrency.Infrastructure.Persistence.Repositories.Implementations.MongoDb;
using Polly;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using OkalaCryptoCurrency.Infrastructure.Persistence.DbContext.MongoDb;

namespace OkalaCryptoCurrency.Api.Utility.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services,string apiKey)
        {
            services.RegisterMongoDatabase();
            services.RegisterServices();
            services.RegisterDomainServices();
            services.RegisterRepositories();
            services.AddSingleton<ILoggerService, LoggerService>();
            services.RegisterMemoryCache();
            services.RegisterApiClient(apiKey);
        }
        private static void RegisterMemoryCache(this IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();
            services.AddMemoryCache();
        }
        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICryptoCurrencyService, CryptoCurrencyService>();
        }
        private static void RegisterDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ICryptoCurrencyDomainService, CryptoCurrencyDomainService>();
        }
        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICryptoCurrencyRepository, CryptoCurrencyRepository>();
        }
        private static void RegisterMongoDatabase(this IServiceCollection services)
        {
            services.AddScoped<MongoDbContext>();
        }
        private static void RegisterApiClient(this IServiceCollection services , string apiKey)
        {

            services.AddHttpClient<ICryptoCurrencyApiClient, CryptoCurrencyApiClient>(client =>
            {
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddTransientHttpErrorPolicy(policy => policy.RetryAsync(3));
        }

    }
}
