namespace Ebay.Domain.Abstractions;

public class Error
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    public static Error None = new(string.Empty, string.Empty);
    public string Code { get; private set; }
    public string Message { get; private set; }
}
