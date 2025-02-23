using System.Text.Json.Serialization;

namespace CoffeeShop.Entities.GroupBuyer;

public class Address
{
    [JsonIgnore]
    public int AddressId { get; private set; }
    [JsonIgnore]
    public int BuyerId { get; private set; }
    [JsonIgnore]
    public BuyerUser Buyer { get; private set; }

    public string Street { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public bool IsDefault { get; private set; }

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
