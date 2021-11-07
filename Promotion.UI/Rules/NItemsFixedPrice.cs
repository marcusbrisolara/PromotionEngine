using Promotion.UI.Entities;
using Promotion.UI.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Promotion.UI.Rules
{
    public class NItemsFixedPrice : IRule
    {
        private IEnumerable<NItemsFixedPriceParameters> _promotions;

        public NItemsFixedPrice()
        {
            _promotions = new List<NItemsFixedPriceParameters>
            {
                new NItemsFixedPriceParameters 
                { 
                    NumberOfItems = 3,
                    ItemId = 'A',
                    FixedPrice = 130,
                    Description = "3 A's for 130"
                },
                new NItemsFixedPriceParameters
                {
                    NumberOfItems = 2,
                    ItemId = 'B',
                    FixedPrice = 45,
                    Description = "2 B's for 45"
                }
            };
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
