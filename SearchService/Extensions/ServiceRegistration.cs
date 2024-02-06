using MassTransit;
using SearchService.Consumers;

namespace SearchService.Extensions;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddMassTransit(x =>
        {
            x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
            x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
            x.UsingRabbitMq((context, config) =>
            {
                config.ConfigureEndpoints(context);
            });
        });

    }
}
