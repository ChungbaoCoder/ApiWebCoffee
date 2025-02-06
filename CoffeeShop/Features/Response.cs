namespace CoffeeShop.Features;

public class Response<T>
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public T Data { get; set; }

    public Response(T data, string message = "Request was successful.", int statusCode = 200)
    {
        Data = data;
        Message = message;
        StatusCode = statusCode;
        Success = true;
    }

    public Response(string message, int statusCode = 400)
    {
        Data = default;
        Message = message;
        StatusCode = statusCode;
        Success = false;
    }
}
