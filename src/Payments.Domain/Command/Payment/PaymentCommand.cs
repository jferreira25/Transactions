using MediatR;

namespace Payments.Domain.Command.Payment;

public class PaymentCommand : IRequest<PaymentCommandResponse>
{
    public decimal Value { get; set; }
}