using Promotion.UI.Entities;
using Promotion.UI.Interfaces;
using System.Collections.Generic;

namespace Promotion.UI.Services
{
    public class Engine
    {
        private readonly IEnumerable<IRule> _rules;

        public Engine(IEnumerable<IRule> rules)
        {
            _rules = rules;
        }

        public Cart CheckPromotions(Cart cart)
        {
            foreach (var rule in _rules)
            {
                cart = rule.Apply(cart);
            }
            return cart;
        }
    }
}
