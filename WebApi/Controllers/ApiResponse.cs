namespace Larder.Controllers;

public enum ApiResponseType
{
    Success, Danger, Warning, Info
}

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public string Message { get; set; }
    public ApiResponseType Type { get; set; }

    public ApiResponse(T data, string msg, ApiResponseType type)
    {
        Data = data;
        Message = msg;
        Type = type;
    }

    public ApiResponse(string msg, ApiResponseType type)
    {
        Data = default;
        Message = msg;
        Type = type;
    }
}
