namespace CoffeeShop.Entities.GroupItem;

public class Customization
{
    public int CustomizationId { get; private set; }
    public string MilkType { get; private set; }
    public string SugarLevel { get; private set; }
    public string Temperature { get; private set; }

    public string? _topping { get; private set; }
    public IReadOnlyCollection<string> Topping => (_topping?.Split(',').ToList()) ?? new List<string>();
    public string? _flavor { get; private set; }
    public IReadOnlyCollection<string> Flavor => (_flavor?.Split(',').ToList()) ?? new List<string>();

    public int CoffeeItemId { get; private set; }
    public CoffeeItem CoffeeItem { get; private set; }

    private Customization() { }

    public Customization(string milkType, string sugarLevel, string temperature, IEnumerable<string>? toppings, IEnumerable<string>? flavors)
    {
        MilkType = milkType;
        SugarLevel = sugarLevel;
        Temperature = temperature;
        AddTopping(toppings);
        AddFlavor(flavors);
    }

    public void UpdateCustomization(string milkType, string sugarLevel, string temperature)
    {
        MilkType = milkType;
        SugarLevel = sugarLevel;
        Temperature = temperature;
    }

    public void AddTopping(IEnumerable<string> toppings)
    {
        if (!toppings.Any())
            return;

        foreach(var topping in toppings)
        {
            if (string.IsNullOrWhiteSpace(topping))
                throw new ArgumentException("Topping không được để trống", nameof(topping));
        }

        foreach(var topping in toppings)
        {
            var list = Topping.Append(topping);
            _topping = string.Join(", ", list);
        }
    }

    public void AddFlavor(IEnumerable<string> flavors)
    {
        if (!flavors.Any())
            return;

        foreach (var flavor in flavors)
        {
            if (string.IsNullOrWhiteSpace(flavor))
                throw new ArgumentException("Flavor không được để trống", nameof(flavor));
        }

        foreach (var flavor in flavors)
        {
            var list = Flavor.Append(flavor);
            _flavor = string.Join(", ", list);
        }
    }

    public void RemoveTopping(string topping)
    {
        if (Topping.Contains(topping))
        {
            var list = Topping.Select(t => t != topping).ToList();
            _topping = string.Join(", ", list);
        }
    }

    public void RemoveFlavor(string flavor)
    {
        if (Flavor.Contains(flavor))
        {
            var list = Flavor.Select(t => t != flavor).ToList();
            _flavor = string.Join(", ", list);
        }
    }
}
