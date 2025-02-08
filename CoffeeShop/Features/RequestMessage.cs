using Microsoft.AspNetCore.Diagnostics;

namespace CoffeeShop.Features;

public class RequestMessage
{
    public static Func<string, string> Text => (msg) => string.Concat("Gửi request ", msg.ToLower());
}
