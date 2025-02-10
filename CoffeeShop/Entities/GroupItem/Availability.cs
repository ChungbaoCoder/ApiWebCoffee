using System.Text.Json.Serialization;

namespace CoffeeShop.Entities.GroupItem;

public class Availability
{
    public int AvailabilityId { get; private set; }
    public int StockQuantity {  get; private set; }
    public bool AvailableStatus { get; private set; }
    public DateTime? RestockDate { get; private set; }

    [JsonIgnore]
    public int CoffeeItemId { get; private set; }
    [JsonIgnore]
    public CoffeeItem CoffeeItem { get; private set; }

    private Availability() { }

    public Availability(int stockQuantity,bool availableStatus, DateTime? restockDate)
    {
        StockQuantity = stockQuantity;
        AvailableStatus = availableStatus;
        RestockDate = restockDate;
    }

    public void UpdateQuantity(int stockQuantity)
    {
        if (StockQuantity < 1 || StockQuantity > 99)
            throw new ArgumentOutOfRangeException(nameof(stockQuantity), "Số lượng sản phẩm phải từ 1 - 99.");

        StockQuantity = stockQuantity;
    }

    public void UpdateStatus(bool availableStatus, DateTime? restockDate)
    {
        AvailableStatus = availableStatus;
        RestockDate = restockDate;
    }
}
