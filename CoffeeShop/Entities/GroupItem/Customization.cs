namespace CoffeeShop.Entities.GroupItem;

public class Customization
{
    public int CustomizationId { get; private set; }
    public string MilkType { get; private set; }
    public string SugarLevel { get; private set; }
    public string Temperature { get; private set; }
    private List<string> _topping = new List<string>();
    public IReadOnlyCollection<string> Topping => _topping.AsReadOnly();
    private List<string> _flavor = new List<string>();
    public IReadOnlyCollection<string> Flavor => _flavor.AsReadOnly();

    public int CoffeeItemId { get; private set; }
    public CoffeeItem CoffeeItem { get; private set; }

    private Customization() { }

    public Customization(string milkType, string sugarLevel, string temperature, IEnumerable<string>? topping, IEnumerable<string>? flavor)
    {
        MilkType = milkType;
        SugarLevel = sugarLevel;
        Temperature = temperature;
        _topping = SetNoneIfNoList(topping);
        _flavor = SetNoneIfNoList(flavor);
    }

    private List<string> SetNoneIfNoList(IEnumerable<string>? list)
    {
        return list?.Any() == true ? list.ToList() : new List<string> { "none" };
    }

    public void AddTopping(string topping)
    {
        if (string.IsNullOrWhiteSpace(topping))
            throw new ArgumentException("Topping không được để trống", nameof(topping));

        _topping.RemoveAll(t => string.IsNullOrWhiteSpace(t) || t.Equals("none", StringComparison.OrdinalIgnoreCase));
        _topping.Add(topping);
    }

    public void AddFlavor(string flavor)
    {
        if (string.IsNullOrWhiteSpace(flavor))
            throw new ArgumentException("Flavor không được để trống", nameof(flavor));

        _flavor.RemoveAll(f => string.IsNullOrWhiteSpace(f) || f.Equals("none", StringComparison.OrdinalIgnoreCase));
        _flavor.Add(flavor);
    }
}
