using System.ComponentModel.DataAnnotations;
using CoffeeShop.Entities.GroupItem;

namespace CoffeeShop.Features.Coffee;

public class CoffeeItemRequest
{
    [Required(ErrorMessage = "Cần tên của sản phẩm")]
    [StringLength(100, ErrorMessage = "Tên không được dài quá 100 kí tự.")]
    public string Name { get; set; }
    [StringLength(200, ErrorMessage = "Nội dung không được quá 200 kí tự.")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Cần loại sản phẩm")]
    public string Category { get; set; }
    [Required(ErrorMessage = "Cần kích cỡ sản phẩm")]
    public string Size { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số dương")]
    public decimal Price { get; set; }
    public string PictureUri { get; set; }

    [Required(ErrorMessage = "Cần trạng thái sản phẩm")]
    public bool AvailabilityStatus { get; set; }
    public DateTime? RestockDate { get; set; } = null;
    [Range(0, 99, ErrorMessage = "Số lượng phải là số dương.")]
    public int StockQuantity { get; set; }

    public string MilkType { get; set; }
    public string SugarLevel { get; set; }
    public string Temperature { get; set; }
    public List<string> Topping { get; set; } = new List<string>();
    public List<string> Flavor { get; set; } = new List<string>();
}
