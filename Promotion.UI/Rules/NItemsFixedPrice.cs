using Microsoft.Extensions.Configuration;
using Promotion.UI.Entities;
using Promotion.UI.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Promotion.UI.Rules
{
    public class NItemsFixedPrice : IRule
    {
        private readonly IConfiguration _configuration;
        private IEnumerable<NItemsFixedPriceParameters> _promotions;

        public NItemsFixedPrice(IConfiguration configuration)
        {
            _configuration = configuration;
            _promotions = _configuration.GetSection(nameof(NItemsFixedPriceParameters)).Get<List<NItemsFixedPriceParameters>>();
        }   

        public Cart Apply(Cart cart)
        {
            foreach (var promotion in _promotions)
            {
                if (ShouldApply(cart, promotion))
                {
                    var itemPromotion = cart.CartItems.FirstOrDefault(x => x.Id == promotion.ItemId);
                    var qtyPromotionPrice = itemPromotion.Quantity / promotion.NumberOfItems;
                    var qtyNormalPrice = itemPromotion.Quantity - (qtyPromotionPrice * promotion.NumberOfItems);

                    var discount = itemPromotion.TotalPrice - ((qtyPromotionPrice * promotion.FixedPrice) + qtyNormalPrice * itemPromotion.UnitPrice);

                    cart.AppliedPromotions.Add(new AppliedPromotion(promotion.Description, discount));
                }
            }
            return cart;
        }

        private bool ShouldApply(Cart cart, NItemsFixedPriceParameters promotion)
        {
            return cart.CartItems.FirstOrDefault(x => x.Id == promotion.ItemId).Quantity >= promotion.NumberOfItems;
        }
    }
}
