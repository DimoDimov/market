namespace Market;

public sealed class OrderService
{
    private static readonly OrderService instance = new();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static OrderService()
    {
    }

    private OrderService()
    {
    }

    public static OrderService Instance
    {
        get
        {
            return instance;
        }
    }

    public Decimal GetFinalPrice() => GetTotalPrice() - GetDiscounts();

    public Decimal GetTotalPrice() =>
       throw new NotImplementedException();

    public Decimal GetDiscounts()
    {
        throw new NotImplementedException();
    }
}