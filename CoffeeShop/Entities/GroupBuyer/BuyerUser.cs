using System.Text.Json.Serialization;
using CoffeeShop.Entities.GroupBasket;
using CoffeeShop.Entities.GroupOrder;

namespace CoffeeShop.Entities.GroupBuyer;

public class BuyerUser
{
    public int BuyerId { get; private set; }
    public string? UserGuid { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime DateCreated { get; private set; } = DateTime.Now;
    public DateTime DateUpdated { get; private set; } = DateTime.Now;

    public List<Address> Address = new List<Address>();
    public List<BuyerBasket> Baskets = new List<BuyerBasket>();
    public List<BuyerOrder> Orders = new List<BuyerOrder>();

    //Dành cho tương lai thêm chức năng thanh toán
    //private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();
    //public IReadOnlyCollection<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

    private BuyerUser() { }

    public BuyerUser(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public void UpdateInfo(string name, string email)
    {
        Name = name;
        Email = email;
        DateUpdated = DateTime.Now;
    }

    public void UpdateAddress(int addressId, string street, string city, string state, string country)
    {
        if (!Address.Any(a => a.AddressId == addressId))
        {
            Address.Add(new Address(BuyerId, street, city, state, country));
            return;
        }
        var existAddress = Address.First(a => a.AddressId == addressId);
        existAddress.UpdateAddress(street, city, state, country);
        DateUpdated = DateTime.Now;
    }

    public void SetDefaultAddress(int addressId)
    {
        foreach(var address in Address)
        {
            if (address.AddressId != addressId)
            {
                address.SetDefault(false);
            }
            else
            {
                address.SetDefault(true);
            }
        }
        DateUpdated = DateTime.Now;
    }

    public void RemoveAddress(int addressId)
    {
        var existAddress = Address.FirstOrDefault(a => a.AddressId == addressId);

        if (existAddress != null)
            Address.Remove(existAddress);
    }

    //public void AddPaymentMethod(PaymentMethod paymentMethod)
    //{
    //    if (paymentMethod == null)
    //        throw new ArgumentNullException(nameof(paymentMethod));

    //    _paymentMethods.Add(paymentMethod);
    //}
}
