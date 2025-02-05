namespace CoffeeShop.Entities.GroupOrder;

public class OrderStatus
{
    public string Status { get; private set; }
    public DateTime CompleteTime { get; private set; }
    public DateTime LastUpdate { get; private set; } = DateTime.Now;

    private OrderStatus() { }

    public OrderStatus(string status, DateTime completeTime)
    {
        SetStatus(status);
        CompleteTime = completeTime;
    }

    public void SetStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            Status = "Đang giao";
        }
        else
        {
            Status = status;
        }
        LastUpdate = DateTime.Now;
    }
}
