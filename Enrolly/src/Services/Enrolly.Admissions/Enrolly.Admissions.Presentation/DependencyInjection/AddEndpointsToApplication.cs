using Enrolly.Admissions.Presentation.Endpoints;

namespace Enrolly.Admissions.Presentation.DependencyInjection;

public static class AddEndpointsToApplication
{
    public static WebApplication AddEndpoints(this WebApplication app)
    {
        app.AddAdmissionEndpoints();
        app.AddAdmissionProgramsEndpoints();
        
        return app;
    }
}