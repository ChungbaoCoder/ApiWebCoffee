namespace CoffeeShop.Entities.GroupOrder;

public class OrderAddress
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }

    private OrderAddress() { }

    public OrderAddress(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }
}
