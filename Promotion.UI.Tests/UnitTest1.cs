using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Promotion.UI.Entities;
using Promotion.UI.Services;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Promotion.UI.Tests
{
    public class Tests
    {
        IConfiguration _configuration;
        
        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)                
                .Build();
        }

        [Test]
        public void ScenarioA_NoPromotionsToApply()
        {
            EngineService engineService = new EngineService(_configuration);
            var cart = engineService.ProcessPromotions(TestData.ScenarioA_Cart);

            Assert.AreEqual(100, cart.FinalTotalPrice);
        }

        [Test]
        public void ScenarioB_PromotionsApplied()
        {
            EngineService engineService = new EngineService(_configuration);
            var cart = engineService.ProcessPromotions(TestData.ScenarioB_Cart);

            Assert.AreEqual(370, cart.FinalTotalPrice);
        }

        [Test]
        public void ScenarioC_PromotionsApplied()
        {
            EngineService engineService = new EngineService(_configuration);
            var cart = engineService.ProcessPromotions(TestData.ScenarioC_Cart);

            Assert.AreEqual(280, cart.FinalTotalPrice);
        }

        [Test]
        public void EmptyCart()
        {
            EngineService engineService = new EngineService(_configuration);
            var cart = engineService.ProcessPromotions(TestData.EmptyCart);

            Assert.AreEqual(0, cart.FinalTotalPrice);
        }

        [Test]
        public void NullCart()
        {
            EngineService engineService = new EngineService(_configuration);
            var cart = engineService.ProcessPromotions(null);

            Assert.IsNull(cart);
        }
    }
}