using System.Text.Json.Serialization;
using CoffeeShop.Entities.GroupBasket;
using CoffeeShop.Entities.GroupOrder;
using CoffeeShop.Infrastructure.Auth;

namespace CoffeeShop.Entities.GroupBuyer;

public class BuyerUser
{
    public int BuyerId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNum { get; private set; } = string.Empty;
    public DateTime DateJoined { get; private set; } = DateTime.Now;
    public List<Address> Address { get; private set; }

    [JsonIgnore]
    public BuyerBasket Baskets { get; private set; }
    [JsonIgnore]
    public List<BuyerOrder> Orders = new List<BuyerOrder>();
    [JsonIgnore]
    public DateTime? DeletedAt { get; private set; }
    [JsonIgnore]
    public virtual CustomerAuth CustomerAuth { get; private set; }

    //Dành cho tương lai thêm chức năng thanh toán
    //private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();
    //public IReadOnlyCollection<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

    private BuyerUser() { Address = new List<Address>(); }

    public BuyerUser(string name, string email, string phoneNum)
    {
        UpdateInfo(name, email, phoneNum);
        Address = new List<Address>();
    }

    public void UpdateInfo(string name, string email, string phoneNum)
    {
        Name = name;
        Email = email;
        PhoneNum = phoneNum;
    }

    public void AddAddress(Address address)
    {
        Address.Add(address);
    }

    public void SetDefaultAddress(int addressId)
    {
        if (Address.Count == 1)
        {
            foreach (var address in Address)
                address.SetDefault(true);
        }
        else
        {
            foreach (var address in Address)
            {
                if (address.AddressId != addressId)
                    address.SetDefault(false);
                else
                    address.SetDefault(true);
            }
        }
    }

    public void MarkDeletion()
    {
        DeletedAt = DateTime.Now;
    }

    public void UnMarkDeletion()
    {
        DeletedAt = null;
    }

    //public void AddPaymentMethod(PaymentMethod paymentMethod)
    //{
    //    if (paymentMethod == null)
    //        throw new ArgumentNullException(nameof(paymentMethod));

    //    _paymentMethods.Add(paymentMethod);
    //}
}
