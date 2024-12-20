namespace OkalaCryptoCurrency.Api.Utility
{
    public record BaseResponse(Status Status, object? Data = null)
    {
    }
    public record Status(IEnumerable<string> ErrorMessages = null, int StatusCode = 0)
    {

    }
}
