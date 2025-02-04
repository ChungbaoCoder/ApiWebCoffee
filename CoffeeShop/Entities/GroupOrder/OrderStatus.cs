namespace CoffeeShop.Entities.GroupOrder;

public class OrderStatus
{
    public int OrderStatusId { get; private set; }
    public int OrderId { get; private set; }
    public string Status { get; private set; }
    public DateTime OrderDate { get; private set; }

    public OrderStatus(int orderId, string status, DateTime orderDate)
    {
        OrderId = orderId;
        SetStatus(status);
        OrderDate = orderDate;
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
        UpdateDate();
    }

    public void UpdateDate()
    {
        OrderDate = DateTime.Now;
    }

    public string Detail()
    {
        return $"Mặt hàng {OrderId}.Hiện {Status} trong ngày {OrderDate:dd/MM/yyyy}";
    }
}
