using Microsoft.Extensions.Logging;
using Payments.Api.DependencyInjection;
using Payments.Domain.Event;
using Payments.Infrastructure.Entities;
using Payments.Infrastructure.Enums;
using Rebus.Handlers;

namespace Payments.Domain.EventHandler;

public class TransactionEventHandler : IHandleMessages<TransactionEvent>
{
    private readonly ILogger<TransactionEventHandler> _logger;
    private readonly ApplicationDbContext _context;

    public TransactionEventHandler(
        ILogger<TransactionEventHandler> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(TransactionEvent message)
    {
        _logger.LogInformation($"{nameof(TransactionEventHandler)}.Handle Inserindo transação no banco.");

        try
        {
            var transaction = await _context.FindOneAsync(message.TransactionId);

            if (transaction is not null)
            {
                _logger.LogInformation($"Transação {message.TransactionId} já foi processada.");

                return;
            }

            await _context.AddAsync(new Payment()
            {
                TransactionId = message.TransactionId,
                Value = message.Value,
                Status = GetRandomPaymentStatus(message.Value)
            });
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Ocorreu um erro ao inserir a transação no banco");
        }

    }

    public static PaymentStatus GetRandomPaymentStatus(decimal value)
    {
        if (value <= 0)
        {
            return PaymentStatus.Declined;
        }

        PaymentStatus[] statusValues = (PaymentStatus[])Enum.GetValues(typeof(PaymentStatus));

        Random random = new Random();
        int randomIndex = random.Next(0, statusValues.Length - 1);

        return statusValues[randomIndex];
    }
}