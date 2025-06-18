public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Exception? Error { get; }

    private Result(bool isSuccess, T? value, Exception? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T? value) => new Result<T>(true, value, null);

    public static Result<T> Failure(Exception error)
    {
        if (error == null)
            throw new ArgumentNullException(nameof(error));

        return new Result<T>(false, default, error);
    }
}
