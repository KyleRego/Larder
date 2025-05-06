using System.Text.Json.Serialization;

namespace Larder.Controllers;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public string Message { get; set; }
    public ApiResponseType Type { get; set; }

    [JsonConstructor]
    public ApiResponse(T data, string message, ApiResponseType type)
    {
        Data = data;
        Message = message;
        Type = type;
    }

    public ApiResponse(string msg, ApiResponseType type)
    {
        Data = default;
        Message = msg;
        Type = type;
    }
}
