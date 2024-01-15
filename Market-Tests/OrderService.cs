using Market;

namespace Market_Tests;

public class Tests
{
    Product? requiredProductOffer1;
    Product? discountedProductOffer1;
    Offer? offer1;

    Product? requiredProductOffer2;
    Product? discountedProductOffer2;
    Offer? offer2;

    [SetUp]
    public void Setup()
    {
        requiredProductOffer1 = new() { Name = "Butter", Price = 0.80M, Quantity = 2 };
        discountedProductOffer1 = new() { Name = "Bread", Price = 1.00M, Quantity = 1 };
        offer1 = new Offer() { RequiredProduct = requiredProductOffer1, DiscountedProduct = discountedProductOffer1 };

        requiredProductOffer2 = new() { Name = "Milk", Price = 1.15M, Quantity = 4 };
        discountedProductOffer2 = new() { Name = "Milk", Price = 1.10M, Quantity = 1 };
        offer2 = new Offer() { RequiredProduct = requiredProductOffer2, DiscountedProduct = discountedProductOffer2 };
    }

    [Test]
    public void OrderedOneButterOneBreadOneMilksShouldTotalCorrect()
    {
        OrderService orderService = OrderService.Instance;

        Assert.Multiple(() =>
        {
            Assert.That(orderService.GetTotalPrice(), Is.EqualTo(2.95M));
            Assert.That(orderService.GetDiscounts(), Is.EqualTo(0M));
            Assert.That(orderService.GetFinalPrice(), Is.EqualTo(2.95M));
        });
    }

    [Test]
    public void OrderedTwoButtersTwoBreadsShouldCalculateTheTotalWithOffersApplied()
    {
        OrderService orderService = OrderService.Instance;

        Assert.Multiple(() =>
        {
            Assert.That(orderService.GetTotalPrice(), Is.EqualTo(3.60M));
            Assert.That(orderService.GetDiscounts(), Is.EqualTo(0.50M));
            Assert.That(orderService.GetFinalPrice(), Is.EqualTo(3.10M));
        });
    }

    [Test]
    public void OrderedFourBreadsShouldCalculateTheTotalWithOffersApplied()
    {
        OrderService orderService = OrderService.Instance;

        Assert.Multiple(() =>
        {
            Assert.That(orderService.GetTotalPrice(), Is.EqualTo(4.60M));
            Assert.That(orderService.GetDiscounts(), Is.EqualTo(1.15M));
            Assert.That(orderService.GetFinalPrice(), Is.EqualTo(3.45M));
        });
    }

    [Test]
    public void OrderedTwoButtersOneBreadEightMilksShouldTotalCorrect()
    {
        OrderService orderService = OrderService.Instance;

        Assert.Multiple(() =>
        {
            Assert.That(orderService.GetTotalPrice(), Is.EqualTo(11.80M));
            Assert.That(orderService.GetDiscounts(), Is.EqualTo(2.80M));
            Assert.That(orderService.GetFinalPrice(), Is.EqualTo(9.00M));
        });
    }
}