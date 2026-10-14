using Ebay.Domain.Abstractions;

namespace Ebay.Api.Controllers;

public class ApiResponse
{
    public ApiResponse()
    {
        Ok = true;
        Error = Error.None;
    }

    public ApiResponse(Error error)
    {
        Error = error;
    }

    public ApiResponse(Error error, List<string> validationErrors)
    {
        Error = error;
        ValidationErrors = validationErrors;
    }

    public bool Ok { get; private set; }
    public Error Error {  get; private set; }
    public List<string> ValidationErrors { get; private set; } = [];
}

public class ApiResponse<TResult> : ApiResponse
{
    public ApiResponse(TResult result) : base()
    {
        Result = result;
    }
    public TResult Result { get; private set; }
}
