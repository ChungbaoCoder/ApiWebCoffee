namespace CoffeeShop.Entities.GroupBuyer;

public class Address
{
    public int AddressId { get; private set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public bool IsDefault { get; private set; }

    public int BuyerId { get; private set; }
    public Buyer Buyer { get; private set; }

    private Address() { }

    public Address(int buyerId, string street, string city, string state, string country)
    {
        UpdateAddress(street, city, state, country);
    }

    public void UpdateAddress(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }

    public void SetDefault(bool isDefault)
    {
        IsDefault = isDefault;
    }
}
