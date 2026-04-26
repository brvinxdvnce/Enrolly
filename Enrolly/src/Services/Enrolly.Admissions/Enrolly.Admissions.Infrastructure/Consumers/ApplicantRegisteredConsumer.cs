using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events;
using Enrolly.Contracts.Events.Events;
using MassTransit;

namespace Enrolly.Admissions.Infrastructure.Consumers;

public class ApplicantRegisteredConsumer : IConsumer<ApplicantRegisteredEvent>
{
    private readonly IApplicantRepository _applicantRepository;

    public ApplicantRegisteredConsumer(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }

    public async Task Consume(ConsumeContext<ApplicantRegisteredEvent> context)
    {
        var applicant = new Applicant()
        {
            Id = context.Message.ApplicantId,
            Name = context.Message.ApplicantName,
            Email = context.Message.Email
        };
        
        await _applicantRepository.Add(applicant);
    }
}


