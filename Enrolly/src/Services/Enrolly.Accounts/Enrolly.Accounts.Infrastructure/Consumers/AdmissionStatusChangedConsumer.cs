using Enrolly.Contracts.Events;
using MassTransit;

namespace Enrolly.Accounts.Infrastructure.Consumers;

public class AdmissionStatusChangedConsumer : IConsumer<AdmissionStatusChangedEvent>
{
    public Task Consume(ConsumeContext<AdmissionStatusChangedEvent> context)
    {
        throw new NotImplementedException();
    }
}