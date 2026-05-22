using Microsoft.AspNetCore.Http;

namespace Enrolly.Shared.Logging.Utils.Result;


public static class ResultErrorConverter
{
    public static IResult ToActionResult(this CSharpFunctionalExtensions.Result result)
    {
        if (result.IsSuccess
            || (result.IsFailure && result.Error.StartsWith(ResultError.Ok())))
            return Results.NoContent();
        
        return MapFailure(result.Error);
    }
    
    public static IResult ToActionResult<T>(this CSharpFunctionalExtensions.Result<T> result)
    {
        if (result.IsSuccess
            || (result.IsFailure && result.Error.StartsWith(ResultError.Ok())))
            return Results.Ok(result.Value);

        return MapFailure(result.Error);
    }

    private static IResult MapFailure(string error)
    {
        if (error.StartsWith(ResultError.Conflict()))
            return Results.Conflict(error);
            
        if (error.StartsWith(ResultError.NotFound()))
            return Results.NotFound(error);

        if (error.StartsWith(ResultError.Forbidden()))
            return Results.Forbid();
        
        return Results.BadRequest();
    }
}