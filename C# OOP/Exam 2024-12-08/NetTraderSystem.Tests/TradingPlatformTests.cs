using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace NetTraderSystem.Tests
{
    public class TradingPlatformTests
    {
        [Test]
        public void TradingPlatformShouldBeInitializedSuccessfully()
        {
            TradingPlatform tradingPlatform = new TradingPlatform(10);
            Assert.That(tradingPlatform.Products, Is.Empty);
        }

        [Test]
        public void ProductsShouldBeAddedSuccessfully()
        {
            TradingPlatform tradingPlatform = new TradingPlatform(10);
            List<Product> addedProducts = new List<Product>(capacity: 10);
            
            for (int i = 0; i < 10; i++)
            {
                var product = new Product($"product_{i + 1}", "test", 3.14);

                addedProducts.Add(product);
                var result = tradingPlatform.AddProduct(addedProducts[i]);
                
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo($"Product product_{i + 1} added successfully"));
                    Assert.That(tradingPlatform.Products, Is.EqualTo(addedProducts));
                });
            }
        }

        [Test]
        public void ProductsShouldNotBeAddedIfLimitIsReached()
        {
            TradingPlatform tradingPlatform = new TradingPlatform(0);

            string result = tradingPlatform.AddProduct(new Product("should_not_be_possible", "test", 10.9));
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo("Inventory is full"));
                Assert.That(tradingPlatform.Products, Is.Empty);
            });
        }

        [Test]
        public void RemoveShouldReturnFalseIfProductDoesNotExist()
        {
            TradingPlatform tradingPlatform = new TradingPlatform(10);

            var product = new Product("p1", "test", 984516);
            tradingPlatform.AddProduct(product);

            var otherProduct = new Product("other", "test", 651);
            bool result = tradingPlatform.RemoveProduct(otherProduct);
            
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.False);
                Assert.That(tradingPlatform.Products, Is.EqualTo(new Product[] { product }));
            });
        }

        [Test]
        public void RemoveShouldReturnTrueIfProductExists()
        {
            TradingPlatform tradingPlatform = new TradingPlatform(10);

            var product = new Product("p1", "test", 984516);
            tradingPlatform.AddProduct(product);

            bool result = tradingPlatform.RemoveProduct(product);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(tradingPlatform.Products, Is.Empty);
            });
        }

        [Test]
        public void SellShouldReturnNullIfProductDoesNotExist()
        {
            TradingPlatform tradingPlatform = new TradingPlatform(10);

            var product = new Product("p1", "test", 984516);
            tradingPlatform.AddProduct(product);

            var otherProduct = new Product("other", "test", 651);
            Product result = tradingPlatform.SellProduct(otherProduct);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Null);
                Assert.That(tradingPlatform.Products, Is.EqualTo(new Product[] { product }));
            });
        }

        [Test]
        public void SellShouldReturnTheProductIfItExists()
        {
            TradingPlatform tradingPlatform = new TradingPlatform(10);

            var product = new Product("p1", "test", 984516);
            tradingPlatform.AddProduct(product);

            Product result = tradingPlatform.SellProduct(product);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.SameAs(product));
                Assert.That(result, Is.EqualTo(product));
                Assert.That(tradingPlatform.Products, Is.Empty);
            });
        }

        [Test]
        public void InventoryReportShouldBeGeneratedSuccessfullyForEmptyPlatform()
        {
            TradingPlatform tradingPlatform = new TradingPlatform(0);
            string result = tradingPlatform.InventoryReport();

            StringBuilder expectedOutput = new StringBuilder();
            expectedOutput.AppendLine("Inventory Report:");
            expectedOutput.Append("Available Products: 0");

            Assert.That(result, Is.EqualTo(expectedOutput.ToString()));
        }

        [Test]
        public void InventoryReportShouldBeGeneratedSuccessfullyForFullPlatform()
        {
            TradingPlatform tradingPlatform = new TradingPlatform(10);

            var product = new Product("product_1", "test", 3.14);
            tradingPlatform.AddProduct(product);

            string result = tradingPlatform.InventoryReport();

            StringBuilder expectedOutput = new StringBuilder();
            expectedOutput.AppendLine("Inventory Report:");
            expectedOutput.AppendLine("Available Products: 1");
            expectedOutput.Append("Name: product_1, Category: test - $3.14");

            Assert.That(result, Is.EqualTo(expectedOutput.ToString()));
        }
    }
}