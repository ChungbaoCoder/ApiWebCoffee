namespace CoffeeShop.Features;

public class Response<T>
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int StatusCode { get; set; }
    public string Detail { get; set; }
    public T Data { get; set; }

    public Response(T data, string type, string title, string message, int statusCode = 200)
    {
        Type = type;
        Title = title;
        StatusCode = statusCode;
        Detail = message;
        Data = data;
    }

    public Response(string type, string title, string message, int statusCode = 400)
    {
        Type = type;
        Title = title;
        StatusCode = statusCode;
        Detail = message;
    }
}
