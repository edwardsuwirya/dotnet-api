namespace MySimpleNetApi.Utils;

public class CommonResponse<T>
{
    public string StatusCode { get; private set; }
    public string Message { get; private set; }
    public T Data { get; private set; }

    public CommonResponse(T resource)
    {
        StatusCode = "00";
        Message = "SUCCESS";
        Data = resource;
    }

    public CommonResponse(string statusCode, string message = "FAILED")
    {
        StatusCode = statusCode;
        Message = message;
        Data = default;
    }
}