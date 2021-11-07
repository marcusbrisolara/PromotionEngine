using Microsoft.Extensions.Configuration;
using Promotion.UI.Entities;
using Promotion.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.UI.Rules
{
    public class OneAndTwoFixedPrice : IRule
    {
        private readonly IConfiguration _configuration;
        private IEnumerable<OneAndTwoFixedPriceParameters> _promotions;
        private bool arePromotionsMutuallyExclusive = false;

        public OneAndTwoFixedPrice(IConfiguration configuration)
        {
            _configuration = configuration;
            _promotions = _configuration.GetSection(nameof(OneAndTwoFixedPriceParameters)).Get<List<OneAndTwoFixedPriceParameters>>() ?? new List<OneAndTwoFixedPriceParameters>();
        }
        
        public Cart Apply(Cart cart)
        {

            foreach (var promotion in _promotions)
            {
                if (ShouldApply(cart, promotion))
                {
                    var firstItemPromotion = cart.CartItems.FirstOrDefault(x => x.Id == promotion.FirstItemId);
                    var secondItemPromotion = cart.CartItems.FirstOrDefault(x => x.Id == promotion.SecondItemId);

                    var qtyFirstItem = cart.CartItems.Count(x => x.Id == promotion.FirstItemId);
                    var qtySecondItem = cart.CartItems.Count(x => x.Id == promotion.SecondItemId);
                    var qtyPromotionPrice = Math.Min(qtyFirstItem, qtySecondItem);

                    var discount =
                        (qtyPromotionPrice * firstItemPromotion.UnitPrice + qtyPromotionPrice * secondItemPromotion.UnitPrice) - qtyPromotionPrice * promotion.FixedPrice;

                    cart.AppliedPromotions.Add(new AppliedPromotion(promotion.Description, discount));
                }
            }
            return cart;
        }

        private bool ShouldApply(Cart cart, OneAndTwoFixedPriceParameters promotion)
        {
            return arePromotionsMutuallyExclusive
                ? (cart.AppliedPromotions.Count == 0) && (cart.CartItems.Any(x => x.Id == promotion.FirstItemId) && cart.CartItems.Any(x => x.Id == promotion.SecondItemId))
                : (cart.CartItems.Any(x => x.Id == promotion.FirstItemId) && cart.CartItems.Any(x => x.Id == promotion.SecondItemId));
        }
    }
}
