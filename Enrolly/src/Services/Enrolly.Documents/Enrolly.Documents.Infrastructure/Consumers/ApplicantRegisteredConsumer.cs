using Enrolly.Contracts.Events;
using MassTransit;

namespace Enrolly.Documents.Infrastructure.Consumers;

public class ApplicantRegisteredConsumer : IConsumer<ApplicantRegisteredEvent>
{
    public Task Consume(ConsumeContext<ApplicantRegisteredEvent> context)
    {
        throw new NotImplementedException();
    }
}