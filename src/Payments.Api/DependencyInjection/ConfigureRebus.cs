using Payments.Domain.Event;
using Payments.Domain.EventHandler;
using Rebus.Config;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;

namespace Payments.Api.DependencyInjection;

public static class ConfigureRebus
{
    public static IServiceCollection AddRebus(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRebus((configure, provider) =>
        {
            return configure.Transport(t =>
            {
                var connectionString = configuration["ConnectionsString:RabbitMQ"];
                var queueName = $"Payments.{configuration["RabbitMQSettings:QueueName"]}";

                var opt = t.UseRabbitMq(connectionString, queueName)
                    .ClientConnectionName(queueName);

            })
            .Options(options =>
            {
                options.RetryStrategy(
                   secondLevelRetriesEnabled: true,
                   maxDeliveryAttempts: int.Parse(configuration["RabbitMQSettings:MaxDeliveryAttempts"]),
                   errorQueueName: configuration["RabbitMQSettings:ErrorQueueName"]);
                options.SetMaxParallelism(int.Parse(configuration["RabbitMQSettings:MaxParallelism"]));
                options.SetNumberOfWorkers(int.Parse(configuration["RabbitMQSettings:NumberOfWorkers"]));
            }).Routing(r => r.TypeBased()
            .Map<TransactionEvent>($"Payments.{configuration["RabbitMQSettings:QueueName"]}")); ;

        });

        services.AutoRegisterHandlersFromAssemblyOf<TransactionEventHandler>();
       
        return services;

    }

}