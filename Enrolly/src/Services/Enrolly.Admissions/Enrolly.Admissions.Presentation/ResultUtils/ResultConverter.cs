using CSharpFunctionalExtensions;
using Enrolly.Shared.Logging.Utils.Result;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Enrolly.Admissions.Presentation.ResultUtils;

public static class ResultErrorConverter
{
    public static IResult ToActionResult(this Result result)
    {
        if (result.IsSuccess
            || (result.IsFailure && result.Error.StartsWith(ResultError.Ok())))
            return Results.NoContent();
        
        return MapFailure(result.Error);
    }
    
    public static IResult ToActionResult<T>(this Result<T> result)
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