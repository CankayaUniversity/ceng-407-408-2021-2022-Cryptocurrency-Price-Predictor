using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Shared.Logging
{
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
            (context, configuration) =>
            {
                var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Url");
                //var elasticUsername = context.Configuration.GetValue<string>("ElasticConfiguration:Username");
                //var elasticPassword = context.Configuration.GetValue<string>("ElasticConfiguration:Password");

                configuration
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                    .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("specific error"))
                    .MinimumLevel.Override("Quartz", LogEventLevel.Information)
                    .MinimumLevel.Override("MassTransit", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(new Uri(elasticUri))
                        {
                            IndexFormat = $"fantezzie-{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                            AutoRegisterTemplate = true,
                            //ModifyConnectionSettings = x => x.BasicAuthentication(elasticUsername, elasticPassword),
                            CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                            //NumberOfShards = 2,
                            //NumberOfReplicas = 1
                        })
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                    .Enrich.With<LogEnricher>()
                    .ReadFrom.Configuration(context.Configuration);
                        
            };
    }
}
