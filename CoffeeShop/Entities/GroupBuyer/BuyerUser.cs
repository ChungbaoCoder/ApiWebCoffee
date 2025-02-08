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
    
    private List<Address> _address = new List<Address>();
    public IReadOnlyCollection<Address> Address => _address.AsReadOnly();
    private List<BuyerBasket> _basket = new List<BuyerBasket>();
    public IReadOnlyCollection<BuyerBasket> Baskets => _basket.AsReadOnly();
    private List<BuyerOrder> _order = new List<BuyerOrder>();
    public IReadOnlyCollection<BuyerOrder> Order => _order.AsReadOnly();

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
            _address.Add(new Address(BuyerId, street, city, state, country));
            return;
        }
        var existAddress = Address.First(a => a.AddressId == addressId);
        existAddress.UpdateAddress(street, city, state, country);
        DateUpdated = DateTime.Now;
    }

    public void SetDefaultAddress(int addressId)
    {
        foreach(var address in _address)
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
        var existAddress = _address.FirstOrDefault(a => a.AddressId == addressId);

        if (existAddress != null)
            _address.Remove(existAddress);
    }

    //public void AddPaymentMethod(PaymentMethod paymentMethod)
    //{
    //    if (paymentMethod == null)
    //        throw new ArgumentNullException(nameof(paymentMethod));

    //    _paymentMethods.Add(paymentMethod);
    //}
}
