using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Payments.Infrastructure.Entities;
using Payments.Infrastructure.Interfaces;

namespace Payments.Api.DependencyInjection;

public static class DependenciesConfiguration
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        var mongoClient = new MongoClient(builder.Configuration["ConnectionsString:MongoDB"]);
        builder.Services.AddSingleton(mongoClient);

        builder.Services.AddScoped(serviceProvider =>
        {
            var client = serviceProvider.GetRequiredService<MongoClient>();
            return client.GetDatabase(builder.Configuration["MongoDB:DatabaseName"]);
        });

        builder.Services.AddDbContext<ApplicationDbContext>();

        builder.Services.AddScoped<IBaseRepository<Payment>, ApplicationDbContext>();
    }
}