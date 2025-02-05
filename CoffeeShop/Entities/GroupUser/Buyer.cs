namespace CoffeeShop.Entities.GroupUser;

public class Buyer
{
    public int BuyerId { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime DateCreated { get; private set; } = DateTime.Now;
    public DateTime DateUpdated { get; private set; } = DateTime.Now;
    
    private List<Address> _address = new List<Address>();
    public IReadOnlyCollection<Address> Address => _address.AsReadOnly();
    //private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();
    //public IReadOnlyCollection<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

    private Buyer() { }

    public Buyer(string name, string email)
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

    public void UpdateAddress(int addressId, string street, string city, string state, string country, bool isDefault)
    {
        if (!Address.Any(a => a.AddressId == addressId))
        {
            _address.Add(new Address(BuyerId, street, city, state, country, isDefault));
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

    //public void AddPaymentMethod(PaymentMethod paymentMethod)
    //{
    //    if (paymentMethod == null)
    //        throw new ArgumentNullException(nameof(paymentMethod));

    //    _paymentMethods.Add(paymentMethod);
    //}
}
