using Microsoft.Extensions.Configuration;
using Promotion.UI.Entities;
using Promotion.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Promotion.UI.Services
{
    public class EngineService : IEngineService
    {
        private readonly IConfiguration _configuration;
        public EngineService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Cart ProcessPromotions(Cart cart)
        {
            var ruleType = typeof(IRule);
            IEnumerable<IRule> rules = this.GetType().Assembly.GetTypes()
                .Where(p => ruleType.IsAssignableFrom(p) && !p.IsInterface)
                .Select(r => Activator.CreateInstance(r, _configuration) as IRule);
            var engine = new Engine(rules);
            return engine.CheckPromotions(cart);
        }
    }
}
