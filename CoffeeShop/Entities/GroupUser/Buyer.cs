namespace CoffeeShop.Entities.GroupUser;

public class Buyer
{
    public int BuyerId { get; private set; }
    public string Guid { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime DateUpdated { get; private set; }


    private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();
    public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

    private Buyer() { }

    public Buyer(string guid, string name, string email)
    {
        Guid = guid;
        Name = name;
        Email = email;
        DateCreated = DateTime.Now;
        DateUpdated = DateTime.Now;
    }

    public void AddPaymentMethod(PaymentMethod paymentMethod)
    {
        if (paymentMethod == null)
            throw new ArgumentNullException(nameof(paymentMethod));

        _paymentMethods.Add(paymentMethod);
    }

    public void UpdateInfo(string name, string email)
    {
        Name = name;
        Email = email;
        DateUpdated = DateTime.Now;
    }
}
