namespace Larder.Controllers;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public string Message { get; set; }
    public ApiResponseType Type { get; set; }

    // This parameterless constructor is needed for System.Text.Json deserialization
    public ApiResponse()
    {
        Message = "";
    }

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
