using Enrolly.Admissions.Infrastructure.Consumers.Dictionary.EducationLevel;
using Enrolly.Admissions.Infrastructure.Consumers.Dictionary.Faculty;
using Enrolly.Admissions.Infrastructure.Consumers.Dictionary.Program;
using Enrolly.Admissions.Infrastructure.Consumers.Documents;
using Enrolly.Admissions.Infrastructure.Consumers.Users;
using MassTransit;

namespace Enrolly.Admissions.Infrastructure.DependencyInjection;

public static class ReceiveEndpointConfigurationExtensions
{
    public static void ConfigureAdmissionsEndpoints(
        this IReceiveConfigurator configurator,
        IBusRegistrationContext context)
    {
        configurator.ReceiveEndpoint("Admissions.DictionaryEvents", e =>
        {
            e.ConfigureConsumer<ProgramCreatedConsumer>(context);
            e.ConfigureConsumer<ProgramUpdatedConsumer>(context);
            e.ConfigureConsumer<ProgramDeletedConsumer>(context);
            
            e.ConfigureConsumer<EducationLevelCreatedConsumer>(context);
            e.ConfigureConsumer<EducationLevelUpdatedConsumer>(context);
            e.ConfigureConsumer<EducationLevelDeletedConsumer>(context);
            
            e.ConfigureConsumer<FacultyCreatedConsumer>(context);
            e.ConfigureConsumer<FacultyUpdatedConsumer>(context);
            e.ConfigureConsumer<FacultyDeletedConsumer>(context);
        });
        
        configurator.ReceiveEndpoint("Admissions.DocumentEvents", e =>
        {
            e.ConfigureConsumer<DocumentUploadedConsumer>(context);
            e.ConfigureConsumer<DocumentDeletedConsumer>(context);
        });
        
        configurator.ReceiveEndpoint("Admissions.UserEvents", e =>
        {
            e.ConfigureConsumer<ApplicantRegisteredConsumer>(context);
            e.ConfigureConsumer<ApplicantDeletedConsumer>(context);
            e.ConfigureConsumer<ManagerRegisteredConsumer>(context);
            e.ConfigureConsumer<ManagerGradeChangedConsumer>(context);
            e.ConfigureConsumer<ManagerDeletedConsumer>(context);
        });
    }
}