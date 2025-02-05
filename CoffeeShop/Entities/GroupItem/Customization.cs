namespace CoffeeShop.Entities.GroupItem;

public class Customization
{
    public string? Option { get; private set; }
    public string? Choices { get; private set; }

    private Customization() { }

    public Customization(string option, string choices)
    {
        Option = option;
        Choices = choices;
    }
}
