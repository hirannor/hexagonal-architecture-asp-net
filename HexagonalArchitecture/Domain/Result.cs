namespace HexagonalArchitecture.Domain;

public class Result
{
    public bool IsSuccess { get; private set; }
    public List<string> Errors { get; private set; }

    private Result(bool isSuccess, List<string> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors ?? [];
    }

    public static Result Success()
    {
        return new Result(true, []);
    }

    public static Result Failure(List<string> errors)
    {
        return new Result(false, errors);
    }
}

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T Value { get; private set; }
    public List<string> Errors { get; private set; }

    private Result(bool isSuccess, T value, List<string> errors)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors ?? [];
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, []);
    }

    public static Result<T> Failure(List<string> errors)
    {
        return new Result<T>(false, default, errors);
    }
}