
using Newtonsoft.Json;
using static ToDoAPI_ASPNET.Models.Utils.Error;

namespace ToDoAPI_ASPNET.Models.Response;

public class ApiResponse<T>
{
    public string Status { get; init; } = null!;
    public string Message { get; init; } = null!;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public T? Data { get; init; }


    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public ErrorType? ErrorType { get; init; }


    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string>? ValidationErrors { get; init; }

    public static ApiResponse<T> SuccessResponse(string message, T? data)
    {
        return new ApiResponse<T>
        {
            Status = "success",
            Message = message,
            Data = data
        };
    }

}
