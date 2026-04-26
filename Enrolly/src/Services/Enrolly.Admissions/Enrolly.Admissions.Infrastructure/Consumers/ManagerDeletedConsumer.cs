using Enrolly.Contracts.Events.Events;
using MassTransit;

namespace Enrolly.Admissions.Infrastructure.Consumers;

public class ManagerDeletedConsumer : IConsumer<ManagerDeletedEvent>
{
    public Task Consume(ConsumeContext<ManagerDeletedEvent> context)
    {
        throw new NotImplementedException();
    }
}