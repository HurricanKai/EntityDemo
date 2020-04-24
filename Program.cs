using System.Threading.Tasks;
using MassTransit;
using MassTransit.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EntityDemo
{
    public class Program
    {
        public static Task Main(string[] args) 
            => CreateHostBuilder(args).RunConsoleAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Test>();
                    services.AddHostedService<BusService>();
                    services.AddSingleton<IEntityManager, LocalEntityManager>();
                    services.AddSingleton<IEntityResolver, EntityResolver>();
                    services.AddScoped<IEntityMetadataSerializer, EntityMetadataSerializer>();
                    
                    services.AddMassTransit((configurator =>
                    {
                        MassTransitSetup.Setup();
                        configurator.SetKebabCaseEndpointNameFormatter();
                        configurator.AddConsumersFromNamespaceContaining<Program>();
                        configurator.AddInMemoryBus(((provider, factoryConfigurator) =>
                        {
                            LogContext.ConfigureCurrentLogContext(provider.GetRequiredService<ILoggerFactory>());
                            factoryConfigurator.ConfigureEndpoints(provider);
                        }));

                        configurator.AddRequestClient<GetEntityMetadataRequest>();
                        configurator.AddRequestClient<CreateEntityRequest>();
                        configurator.AddRequestClient<GetEntityTransformRequest>();
                    }));
                });
    }
}