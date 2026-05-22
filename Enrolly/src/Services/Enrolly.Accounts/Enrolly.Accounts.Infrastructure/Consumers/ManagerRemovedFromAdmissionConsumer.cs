using Enrolly.Accounts.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Admissions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Accounts.Infrastructure.Consumers;

public class ManagerRemovedFromAdmissionConsumer : IConsumer<ManagerRemovedFromAdmissionEvent>
{ 
    private readonly ILogger<ManagerAssignedToAdmissionConsumer> _logger;
    private readonly IManagerRepository _managerRepository;
    private readonly IApplicantRepository _applicantRepository;
    
    public async Task Consume(ConsumeContext<ManagerRemovedFromAdmissionEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Applicant Id: {Applicant}", 
            nameof(ManagerRemovedFromAdmissionEvent),
            context.Message.ApplicantId);
        
        var removed = await _applicantRepository.RemoveManagerFromApplicantAsync(
            context.Message.ApplicantId,
            context.Message.ManagerId);
        
        if (removed)
            _logger.LogInformation("Manager {ManagerId} successfully removed from Applicant {ApplicantId}",
                context.Message.ManagerId, context.Message.ApplicantId);
        else
            _logger.LogWarning("Could not remove manager {ManagerId} from applicant {ApplicantId}: applicant or manager not found",
                context.Message.ManagerId, context.Message.ApplicantId);
    }
}