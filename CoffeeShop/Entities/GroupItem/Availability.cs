namespace CoffeeShop.Entities.GroupItem;

public class Availability
{
    public bool InStock { get; private set; }
    public DateTime? NextBatchTime { get; private set; }

    private Availability() { }

    public Availability(bool inStock, DateTime nextBatchTime)
    {
        InStock = inStock;
        NextBatchTime = nextBatchTime;
    }
}
