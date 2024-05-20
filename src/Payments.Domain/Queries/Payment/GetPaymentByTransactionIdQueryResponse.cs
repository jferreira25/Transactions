namespace Payments.Domain.Queries.Payment;

public class GetPaymentByTransactionIdQueryResponse
{
    public Guid TransactionId { get; set; }

    public string Status { get; set; }
}
