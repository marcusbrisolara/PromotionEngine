using NUnit.Framework;

namespace Promotion.UI.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ScenarioA()
        {
            PromotionEngineService engineService = new PromotionEngineService();
            var cart = engineService.ProcessPromotions(TestData.ScenarioA_Cart);

            Assert.AreEqual(100, cart.FinalTotalPrice);
        }

        [Test]
        public void ScenarioB()
        {
            PromotionEngineService engineService = new PromotionEngineService();
            var cart = engineService.ProcessPromotions(TestData.ScenarioB_Cart);

            Assert.AreEqual(370, cart.FinalTotalPrice);
        }

        [Test]
        public void ScenarioC()
        {
            PromotionEngineService engineService = new PromotionEngineService();
            var cart = engineService.ProcessPromotions(TestData.ScenarioC_Cart);

            Assert.AreEqual(280, cart.FinalTotalPrice);
        }

        [Test]
        public void EmptyCart()
        {
            PromotionEngineService engineService = new PromotionEngineService();
            var cart = engineService.ProcessPromotions(TestData.EmptyCart);

            Assert.AreEqual(0, cart.FinalTotalPrice);
        }
    }
}