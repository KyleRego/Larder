namespace Larder.Controllers;

public enum ApiResponseType
{
    Success,
    Danger,
    Warning,
    Info
}

public class ApiResponse<T>(T data, string msg, ApiResponseType type)
{
    public T Data { get; set; } = data;
    public string Message { get; set; } = msg;
    public ApiResponseType Type { get; set; } = type;
}
