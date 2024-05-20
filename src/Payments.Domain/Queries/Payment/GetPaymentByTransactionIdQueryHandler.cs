using MediatR;
using Microsoft.Extensions.Logging;
using Payments.Api.DependencyInjection;

namespace Payments.Domain.Queries.Payment;

public class GetPaymentByTransactionIdQueryHandler : IRequestHandler<GetPaymentByTransactionIdQuery, GetPaymentByTransactionIdQueryResponse>
{
    private readonly ILogger<GetPaymentByTransactionIdQueryHandler> _logger;
    private readonly ApplicationDbContext _context;

    public GetPaymentByTransactionIdQueryHandler(
        ILogger<GetPaymentByTransactionIdQueryHandler> logger,
      ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<GetPaymentByTransactionIdQueryResponse> Handle(GetPaymentByTransactionIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _context.FindOneAsync(request.TransactionId);

        if (transaction is null)
        {
            _logger.LogInformation($"Transação {request.TransactionId} não encontrada.");

            return null;
        }

        _logger.LogInformation($"Transação {request.TransactionId} encontrada. [Status={transaction.Status.ToString()}]");

        return new GetPaymentByTransactionIdQueryResponse()
        {
            TransactionId = transaction.TransactionId,
            Status = transaction.Status.ToString(),
        };
    }
}