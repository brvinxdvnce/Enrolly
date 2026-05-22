using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Shared.Logging.Utils.Result;

public static class ResultError
{
    public static string Ok(string message = "") => $"OK: {message}";
    public static string Conflict(string message = "") =>  $"Conflict: {message}";
    public static string NotFound(string message = "") =>  $"NotFound: {message}";
    public static string Forbidden(string message = "") =>  $"Forbidden: {message}";
    public static string Internal(string message = "") =>  $"Internal: {message}";
}
