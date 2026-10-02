namespace Ebay.Domain.Abstractions;

public class Result
{
    public Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Error = Error.None;
    }

    public Result(Error error)
    {
        Error = error;
        IsSuccess = false;
    }

    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; private set; }

}
