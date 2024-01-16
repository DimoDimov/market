namespace Market;

public class Product
{
    public required ProductSelector Name { get; set; }
    public required decimal Price { get; set; }
    public required uint Quantity { get; set; }
}