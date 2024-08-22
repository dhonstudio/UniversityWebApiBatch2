using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Infra.Common
{
    public static class HostRegistration
    {
        public static IHostBuilder UseInfraLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseSerilog((context, service, configuration) =>
            {
            configuration.ReadFrom.Configuration(context.Configuration)
            .WriteTo.Logger(log => log.MinimumLevel.Is(Enum.Parse<LogEventLevel>(context.Configuration["Elastic:MinLevel"]!))
            .WriteTo.Elasticsearch([new Uri(context.Configuration["Elastic:Uri"]!)], option =>
            {
                option.DataStream = new DataStreamName("uni-doni", "dotnet", "training");
            },
            transport =>
            {
                transport.Authentication(new BasicAuthentication(context.Configuration["Elastic:Username"]!,
                    context.Configuration["Elastic:Password"]));
            }));
            });
        }
    }
}
