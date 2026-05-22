using Enrolly.Admissions.Infrastructure.Consumers;
using Enrolly.Admissions.Infrastructure.Consumers.Dictionary.EducationDocumentTypes;
using Enrolly.Admissions.Infrastructure.Consumers.Dictionary.EducationLevel;
using Enrolly.Admissions.Infrastructure.Consumers.Dictionary.Faculty;
using Enrolly.Admissions.Infrastructure.Consumers.Dictionary.Program;
using Enrolly.Admissions.Infrastructure.Consumers.Documents;
using Enrolly.Admissions.Infrastructure.Consumers.Users;
using Enrolly.Contracts.Events.Events;
using MassTransit;

namespace Enrolly.Admissions.Infrastructure.DependencyInjection;

public static class ConsumerRegisterExtension
{
    public static IBusRegistrationConfigurator AddConsumers(this IBusRegistrationConfigurator configurator)
    {
        configurator.AddConsumer<ProgramCreatedConsumer>();
        configurator.AddConsumer<ProgramUpdatedConsumer>();
        configurator.AddConsumer<ProgramDeletedConsumer>();
        
        configurator.AddConsumer<EducationLevelCreatedConsumer>();
        configurator.AddConsumer<EducationLevelUpdatedConsumer>();
        configurator.AddConsumer<EducationLevelDeletedConsumer>();
        
        configurator.AddConsumer<FacultyCreatedConsumer>();
        configurator.AddConsumer<FacultyUpdatedConsumer>();
        configurator.AddConsumer<FacultyDeletedConsumer>();
        
        configurator.AddConsumer<EducationDocumentTypeDeletedConsumer>();
        configurator.AddConsumer<EducationDocumentTypeImportedConsumer>();
        configurator.AddConsumer<EducationDocumentTypeUpdatedConsumer>();
        
        configurator.AddConsumer<DocumentUploadedConsumer>();
        configurator.AddConsumer<DocumentDeletedConsumer>();
        
        configurator.AddConsumer<ApplicantRegisteredConsumer>();
        configurator.AddConsumer<ApplicantDeletedConsumer>();
        configurator.AddConsumer<ManagerRegisteredConsumer>();
        configurator.AddConsumer<ManagerGradeChangedConsumer>();
        configurator.AddConsumer<ManagerDeletedConsumer>();
        
        return configurator;
    }
}