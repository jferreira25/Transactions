using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Payments.Api.DependencyInjection;
using Payments.Domain.Queries.Payment;
using Payments.Infrastructure.Entities;
using Payments.Infrastructure.Interfaces;

namespace Unity.Tests
{
    public class GetPaymentByTransactionIdQueryHandlerTests
    {
        private (GetPaymentByTransactionIdQueryHandler, ApplicationDbContext) GetHandler()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseInMemoryDatabase("InMemoryDb"));

            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
            services.AddScoped<IBaseRepository<Payment>, ApplicationDbContext>();

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<ApplicationDbContext>();

            return (new GetPaymentByTransactionIdQueryHandler(NSubstitute.Substitute.For<ILogger<GetPaymentByTransactionIdQueryHandler>>(), context), context);
        }

        [Fact]
        public async void Test_GetPaymentByTransactionId()
        {
            var handler = GetHandler();

            var payment = new Payment()
            {
                TransactionId = Guid.NewGuid(),
                Value = 100
            };

            await handler.Item2.AddAsync(payment);

            var result = await handler.Item1.Handle(new GetPaymentByTransactionIdQuery(payment.TransactionId), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(payment.TransactionId, result.TransactionId);
        }
    }
}
