using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers;

public class ApplicantDeletedConsumer : IConsumer<ApplicantDeletedEvent>
{
    private readonly IApplicantRepository _applicantRepository;
    
    public ApplicantDeletedConsumer(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }

    public async Task Consume(ConsumeContext<ApplicantDeletedEvent> context)
    {
        await _applicantRepository.DeleteById(context.Message.Id);
    }
}