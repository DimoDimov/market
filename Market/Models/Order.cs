namespace Market;

public class Order
{
    public required Dictionary<string, Product> Products { get; set; }

    public required List<Offer> Offers { get; set; } = [];
}
