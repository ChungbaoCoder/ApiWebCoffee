using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Coffee;

public class CoffeeItemRequest
{
    [Required]
    [StringLength(100, ErrorMessage = "Tên không được dài quá 100 kí tự.")]
    public string Name { get; set; }
    [StringLength(200, ErrorMessage = "Nội dung không được quá 200 kí tự.")]
    public string Description { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public string Size { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số dương")]
    public decimal Price { get; set; }
    public string PictureUri { get; set; }

    [Required]
    public bool AvailabilityStatus { get; set; }
    public DateTime? RestockDate { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là số dương.")]
    public int StockQuantity { get; set; }

    public string MilkType { get; set; }
    public string SugarLevel { get; set; }
    public string Temperature { get; set; }
    public List<string> Topping { get; set; } = new List<string>();
    public List<string> Flavor { get; set; } = new List<string>();
}
