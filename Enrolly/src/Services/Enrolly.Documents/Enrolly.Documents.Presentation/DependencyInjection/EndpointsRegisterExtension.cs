using Enrolly.Documents.Presentation.Endpoints;

namespace Enrolly.Documents.Presentation.DependencyInjection;

public static class EndpointsRegisterExtension
{
    public static WebApplication AddEndpoints(this WebApplication app)
    {
        app.AddEducationDocumentEndpoints();
        
        return app;
    }
}