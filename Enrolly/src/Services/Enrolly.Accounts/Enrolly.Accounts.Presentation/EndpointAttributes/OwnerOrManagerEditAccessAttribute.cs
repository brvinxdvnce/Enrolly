using Enrolly.Accounts.Presentation.EndpointFilters;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Accounts.Presentation.EndpointAttributes;

public class OwnerOrManagerEditAccessAttribute : TypeFilterAttribute
{
    public OwnerOrManagerEditAccessAttribute() : base(typeof(OwnerOrManagerEditAccessFilter)) { }
}