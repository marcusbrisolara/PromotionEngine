using NUnit.Framework;
using Promotion.UI.Services;

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
            EngineService engineService = new EngineService();
            var cart = engineService.ProcessPromotions(TestData.ScenarioA_Cart);

            Assert.AreEqual(100, cart.FinalTotalPrice);
        }

        [Test]
        public void ScenarioB()
        {
            EngineService engineService = new EngineService();
            var cart = engineService.ProcessPromotions(TestData.ScenarioB_Cart);

            Assert.AreEqual(370, cart.FinalTotalPrice);
        }

        [Test]
        public void ScenarioC()
        {
            EngineService engineService = new EngineService();
            var cart = engineService.ProcessPromotions(TestData.ScenarioC_Cart);

            Assert.AreEqual(280, cart.FinalTotalPrice);
        }

        [Test]
        public void EmptyCart()
        {
            EngineService engineService = new EngineService();
            var cart = engineService.ProcessPromotions(TestData.EmptyCart);

            Assert.AreEqual(0, cart.FinalTotalPrice);
        }
    }
}