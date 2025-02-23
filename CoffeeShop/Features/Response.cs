using System.Net;

namespace CoffeeShop.Features;

public class Response<T>
{
    public string Type { get; set; }
    public string Title { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string Detail { get; set; }
    public T? Data { get; set; }

    public Response(string type, HttpStatusCode statusCode, string message, T data)
    {
        Type = type;
        Title = statusCode.ToString();
        StatusCode = statusCode;
        Detail = message;
        Data = data;
    }

    public Response(string type, HttpStatusCode statusCode, string message)
    {
        Type = type;
        Title = statusCode.ToString();
        StatusCode = statusCode;
        Detail = message;
    }
}
