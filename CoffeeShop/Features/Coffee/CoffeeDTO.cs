namespace CoffeeShop.Features.Coffee;

public class CoffeeDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Size { get; set; }
    public string Category { get; set; }
    public string PictureUri { get; set; }
    public bool InStock { get; set; }
    public DateTime NextBatchTime { get; set; }
    public string Option { get; set; }
    public string Choices { get; set; }
}
