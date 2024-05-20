using MediatR;

namespace Payments.Domain.Queries.Payment;

public class GetPaymentByTransactionIdQuery : IRequest<GetPaymentByTransactionIdQueryResponse>
{
    public GetPaymentByTransactionIdQuery(Guid transactionId)
    {
        TransactionId = transactionId;
    }

    public Guid TransactionId { get; set; }
}