namespace Payments.Domain.Command.Payment;

public class PaymentCommandResponse
{
    public PaymentCommandResponse(Guid transactionId)
    {
        TransactionId = transactionId;
    }

    public Guid TransactionId { get; set; }
}
