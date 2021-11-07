using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Promotion.UI.Entities;
using Promotion.UI.Services;
using System;
using System.Collections.Generic;

namespace Promotion.UI.Tests
{
    public class Tests
    {
        Mock<IConfiguration> configuration;
        Mock<IConfigurationSection> configurationSection;

        [SetUp]
        public void Setup()
        {
            configuration = new Mock<IConfiguration>();
        }

        [Test]
        public void ScenarioA()
        {
            configuration.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new  Mock<IConfigurationSection>().Object);
            EngineService engineService = new EngineService(configuration.Object);
            var cart = engineService.ProcessPromotions(TestData.ScenarioA_Cart);

            Assert.AreEqual(100, cart.FinalTotalPrice);
        }

        [Test]
        public void ScenarioB()
        {
            EngineService engineService = new EngineService(configuration.Object);
            var cart = engineService.ProcessPromotions(TestData.ScenarioB_Cart);

            Assert.AreEqual(370, cart.FinalTotalPrice);
        }

        [Test]
        public void ScenarioC()
        {
            EngineService engineService = new EngineService(configuration.Object);
            var cart = engineService.ProcessPromotions(TestData.ScenarioC_Cart);

            Assert.AreEqual(280, cart.FinalTotalPrice);
        }

        [Test]
        public void EmptyCart()
        {
            EngineService engineService = new EngineService(configuration.Object);
            var cart = engineService.ProcessPromotions(TestData.EmptyCart);

            Assert.AreEqual(0, cart.FinalTotalPrice);
        }
    }
}