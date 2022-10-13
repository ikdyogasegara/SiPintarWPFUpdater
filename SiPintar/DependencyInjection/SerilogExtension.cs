using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using SiPintar.Utilities;

namespace SiPintar.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class SerilogExtension
    {
        public static IServiceCollection AddSerilogConfiguration(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Error()
                // .WriteTo.File("logs\\error.log", rollingInterval: RollingInterval.Day)
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(AppSetting.ElasticSearchUrl))
                {
                    IndexFormat = "sipintar_error_wpf",
                })
                .CreateLogger();

            services.AddSingleton(Log.Logger);

            return services;
        }
    }
}
