using Microsoft.Extensions.Configuration;
using Promotion.UI.Entities;
using Promotion.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.UI.Rules
{
    public class PercentageOfItemUnitPrice : IRule
    {
        private readonly IConfiguration _configuration;
        private IEnumerable<PercentageOfItemUnitPriceParameters> _promotions;
        private char[] _itemsOnPromotion;
        private int _itemsAvailable;
        private CartItem _itemPromotion;

        public PercentageOfItemUnitPrice(IConfiguration configuration)
        {
            _configuration = configuration;
            _promotions = _configuration.GetSection(nameof(PercentageOfItemUnitPriceParameters)).Get<List<PercentageOfItemUnitPriceParameters>>();
        }
        public Cart Apply(Cart cart)
        {
            foreach (var promotion in _promotions)
            {
                if (ShouldApply(cart, promotion))
                {
                    var discount = (promotion.Percentage / 100) * (_itemPromotion.UnitPrice * _itemsAvailable);

                    _itemsOnPromotion = new[] { _itemPromotion.Id };
                    cart.AppliedPromotions.Add(new AppliedPromotion(promotion.Description, discount, _itemsOnPromotion, _itemsAvailable));
                }
            }
            return cart;
        }

        private bool ShouldApply(Cart cart, PercentageOfItemUnitPriceParameters promotion)
        {
            _itemPromotion = cart.CartItems.FirstOrDefault(x => x.Id == promotion.ItemId);
            if (_itemPromotion != null)
                _itemsAvailable = _itemPromotion.Quantity - cart.AppliedPromotions.Where(x => x.Items.Contains(_itemPromotion.Id)).Sum(s => s.Quantity);
            return  _itemsAvailable > 0;
        }
    }
}
