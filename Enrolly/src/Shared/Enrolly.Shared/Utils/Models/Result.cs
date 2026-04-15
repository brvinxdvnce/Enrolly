namespace Enrolly.Shared.Logging;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Value { get; private set; }
    public string ErrorMessage { get; private set; }
    
    private Result (T value)
    {
        IsSuccess = true;
        Value = value;
        ErrorMessage = "";
    }
    
    private Result (string error)
    {
        IsSuccess = false;
        Value = default;
        ErrorMessage = error;
    }
    
    public static Result<T> Failure(string error) => new (error);
    
    public static Result<T> Success(T value) => new(value);
}