namespace Market;

public class Cart
{
    public required Dictionary<ProductSelector, Product> Products { get; set; }
    public required List<Offer> Offers { get; set; } = [];
}
