using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payments.Api.DependencyInjection;
using Payments.Infrastructure.Entities;
using Payments.Infrastructure.Interfaces;

namespace Unity.Tests
{
    public class DbContextTests
    {
        private  ApplicationDbContext GetDbContext()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseInMemoryDatabase("InMemoryDb"));

            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
            services.AddScoped<IBaseRepository<Payment>, ApplicationDbContext>();

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<ApplicationDbContext>();

            return  context;
        }

        [Fact]
        public async void Test_AddEntity()
        {
            var context = GetDbContext();
            
            var payment = new Payment()
            {
                TransactionId = Guid.NewGuid(),
                Value = 100
            };

            await context.AddAsync(payment);

            var savedPayment = await context.FindOneAsync(payment.TransactionId);

            Assert.NotNull(savedPayment);
            Assert.Equal(payment.Value, savedPayment?.Value);
        }
    }
}