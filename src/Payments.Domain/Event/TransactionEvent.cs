namespace Payments.Domain.Event;
public class TransactionEvent
{
    public Guid TransactionId { get; set; }

    public decimal Value { get; set; }
}
