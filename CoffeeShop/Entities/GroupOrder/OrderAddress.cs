using System.Text.Json.Serialization;

namespace CoffeeShop.Entities.GroupOrder;

public class OrderAddress
{
    public string Street { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;

    private OrderAddress() { }

    public OrderAddress(string street, string city, string state, string country)
    {
        UpdateShippingAddress(street, city, state, country);
    }

    public void UpdateShippingAddress(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }
}
