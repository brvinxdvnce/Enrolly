using Enrolly.Documents.Presentation.EndpointFilters;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Documents.Presentation.Attributes;

public class RequireOwnerOrManagerEditAccessAttribute : TypeFilterAttribute
{
    public RequireOwnerOrManagerEditAccessAttribute() : base(typeof(EducationDocumentEditAccessFilter)) { }
}   