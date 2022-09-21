namespace MySimpleNetApi.Utils;

public class CommonResponse<T>
{
    public string StatusCode { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; }
}