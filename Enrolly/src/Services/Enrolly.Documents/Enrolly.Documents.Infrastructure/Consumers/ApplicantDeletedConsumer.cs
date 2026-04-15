using Enrolly.Contracts.Events;
using MassTransit;

namespace Enrolly.Documents.Infrastructure.Consumers;

public class ApplicantDeletedConsumer : IConsumer<ApplicantDeletedEvent>
{
    public Task Consume(ConsumeContext<ApplicantDeletedEvent> context)
    {
        throw new NotImplementedException();
    }
}