using MediatR;
using Payments.Domain.Event;
using Rebus.Bus;

namespace Payments.Domain.Command.Payment;

public class PaymentCommandHandler : IRequestHandler<PaymentCommand, PaymentCommandResponse>
{
    private readonly IBus _bus;

    public PaymentCommandHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task<PaymentCommandResponse> Handle(PaymentCommand request, CancellationToken cancellationToken)
    {
        var message = new TransactionEvent { TransactionId = Guid.NewGuid(), Value = request.Value };

        await _bus.Send(message);

        return new PaymentCommandResponse(message.TransactionId);
    }
}
