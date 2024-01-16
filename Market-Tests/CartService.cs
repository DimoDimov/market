using Market;

namespace UnitTests
{
    [TestFixture]
    public class OrderTestCase
    {
        Product? requiredProductOffer1;
        Product? discountedProductOffer1;
        Offer? offer1;

        Product? requiredProductOffer2;
        Product? discountedProductOffer2;
        Offer? offer2;

        List<Offer> offers = new List<Offer>();

        // Initializes the data before each test runs.
        [SetUp]
        public void Setup()
        {
            requiredProductOffer1 = new() { Name = ProductSelector.Butter, Price = 0.80M, Quantity = 2 };
            discountedProductOffer1 = new() { Name = ProductSelector.Bread, Price = 0.50M, Quantity = 1 };
            offer1 = new Offer() { RequiredProduct = requiredProductOffer1, DiscountedProduct = discountedProductOffer1 };

            requiredProductOffer2 = new() { Name = ProductSelector.Milk, Price = 1.15M, Quantity = 4 };
            discountedProductOffer2 = new() { Name = ProductSelector.Milk, Price = 1.15M, Quantity = 1 };
            offer2 = new Offer() { RequiredProduct = requiredProductOffer2, DiscountedProduct = discountedProductOffer2 };

            offers = [offer1, offer2];
        }

        [Test]
        public void OrderedOneButterOneBreadOneMilksShouldTotalCorrect()
        {
            Product butter = new() { Name = ProductSelector.Butter, Price = 0.80M, Quantity = 1 };
            Product milk = new() { Name = ProductSelector.Milk, Price = 1.15M, Quantity = 1 };
            Product bread = new() { Name = ProductSelector.Bread, Price = 1.00M, Quantity = 1 };

            Dictionary<ProductSelector, Product> orderedProducts = new() { { butter.Name, butter }, { milk.Name, milk }, { bread.Name, bread } };
            Cart order = new() { Products = orderedProducts, Offers = offers };

            CartService orderService = CartService.Instance;

            Assert.Multiple(() =>
            {
                Assert.That(orderService.GetTotalPrice(orderedProducts), Is.EqualTo(2.95M));
                Assert.That(orderService.GetDiscounts(orderedProducts, offers), Is.EqualTo(0M));
                Assert.That(orderService.GetFinalPrice(orderedProducts, offers), Is.EqualTo(2.95M));
            });
        }

        [Test]
        public void OrderedTwoButtersTwoBreadsShouldCalculateTheTotalWithOffersApplied()
        {
            Product butter = new() { Name = ProductSelector.Butter, Price = 0.80M, Quantity = 2 };
            Product bread = new() { Name = ProductSelector.Bread, Price = 1.00M, Quantity = 2 };

            Dictionary<ProductSelector, Product> orderedProducts = new() { { butter.Name, butter }, { bread.Name, bread } };
            Cart order = new() { Products = orderedProducts, Offers = offers };

            CartService orderService = CartService.Instance;

            Assert.Multiple(() =>
            {
                Assert.That(orderService.GetTotalPrice(orderedProducts), Is.EqualTo(3.60M));
                Assert.That(orderService.GetDiscounts(orderedProducts, offers), Is.EqualTo(0.50M));
                Assert.That(orderService.GetFinalPrice(orderedProducts, offers), Is.EqualTo(3.10M));
            });
        }

        [Test]
        public void OrderedFourMilksShouldCalculateTheTotalWithOffersApplied()
        {
            Product milk = new() { Name = ProductSelector.Milk, Price = 1.150M, Quantity = 4 };

            Dictionary<ProductSelector, Product> orderedProducts = new() { { milk.Name, milk } };
            Cart order = new() { Products = orderedProducts, Offers = offers };

            CartService orderService = CartService.Instance;

            Assert.Multiple(() =>
            {
                Assert.That(orderService.GetTotalPrice(orderedProducts), Is.EqualTo(4.60M));
                Assert.That(orderService.GetDiscounts(orderedProducts, offers), Is.EqualTo(1.15M));
                Assert.That(orderService.GetFinalPrice(orderedProducts, offers), Is.EqualTo(3.45M));
            });
        }

        [Test]
        public void OrderedTwoButtersOneBreadEightMilksShouldTotalCorrect()
        {
            Product butter = new() { Name = ProductSelector.Butter, Price = 0.80M, Quantity = 2 };
            Product milk = new() { Name = ProductSelector.Milk, Price = 1.15M, Quantity = 8 };
            Product bread = new() { Name = ProductSelector.Bread, Price = 1.00M, Quantity = 1 };

            Dictionary<ProductSelector, Product> orderedProducts = new() { { butter.Name, butter }, { milk.Name, milk }, { bread.Name, bread } };
            Cart order = new() { Products = orderedProducts, Offers = offers };

            CartService orderService = CartService.Instance;

            Assert.Multiple(() =>
            {
                Assert.That(orderService.GetTotalPrice(orderedProducts), Is.EqualTo(11.80M));
                Assert.That(orderService.GetDiscounts(orderedProducts, offers), Is.EqualTo(2.80M));
                Assert.That(orderService.GetFinalPrice(orderedProducts, offers), Is.EqualTo(9.00M));
            });
        }
    }
}

