using MassTransit;

namespace SearchService.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, config) =>
                {
                    config.ConfigureEndpoints(context);
                });
            });


        }
    }
}
