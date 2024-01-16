namespace Market;

public sealed class CartService
{
    private static readonly CartService instance = new();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static CartService()
    {
    }

    private CartService()
    {
    }

    public static CartService Instance
    {
        get
        {
            return instance;
        }
    }

    public Decimal GetFinalPrice(Dictionary<ProductSelector, Product> Products, List<Offer> Offers) => GetTotalPrice(Products) - GetDiscounts(Products, Offers);

    public Decimal GetTotalPrice(Dictionary<ProductSelector, Product> Products) =>
        Products.Aggregate(0M, (acc, product) => acc + product.Value.Price * product.Value.Quantity);

    public Decimal GetDiscounts(Dictionary<ProductSelector, Product> Products, List<Offer> Offers)
    {
        Decimal totalDiscount = 0M;

        Offers.ForEach((offer) =>
        {
            uint availableDiscountedProductQty = 0;
            uint orderedDiscountedProductQty = 0;

            var orderedDiscountedProduct = Products.GetValueOrDefault(offer.DiscountedProduct.Name);
            var orderedRequiredProduct = Products.GetValueOrDefault(offer.RequiredProduct.Name);

            if (orderedRequiredProduct != null && orderedDiscountedProduct != null)
            {
                availableDiscountedProductQty = (uint)Math.Floor((double)orderedRequiredProduct.Quantity / offer.RequiredProduct.Quantity);
                orderedDiscountedProductQty = orderedDiscountedProduct.Quantity;

                availableDiscountedProductQty = availableDiscountedProductQty > orderedDiscountedProductQty ?
                    orderedDiscountedProductQty : availableDiscountedProductQty;


                if (availableDiscountedProductQty >= 1 && orderedRequiredProduct.Quantity >= offer.RequiredProduct.Quantity)
                {
                    totalDiscount += availableDiscountedProductQty * offer.DiscountedProduct.Price;
                }
            }
        });

        return totalDiscount;

    }
}