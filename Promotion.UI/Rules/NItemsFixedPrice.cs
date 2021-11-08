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
        private char[] _itemsOnPromotion;
        private int _itemsAvailable;
        private CartItem _itemPromotion;

        public NItemsFixedPrice(IConfiguration configuration)
        {
            _configuration = configuration;
            _promotions = _configuration.GetSection("NItemsFixedPriceParameters").Get<List<NItemsFixedPriceParameters>>() ?? new List<NItemsFixedPriceParameters>();
        }   

        public Cart Apply(Cart cart)
        {
            foreach (var promotion in _promotions)
            {
                if (ShouldApply(cart, promotion))
                {
                    var qtyPromotionPrice = _itemsAvailable / promotion.NumberOfItems;
                    var qtyNormalPrice = _itemPromotion.Quantity - (qtyPromotionPrice * promotion.NumberOfItems);

                    var discount = _itemPromotion.TotalPrice - ((qtyPromotionPrice * promotion.FixedPrice) + qtyNormalPrice * _itemPromotion.UnitPrice);

                    _itemsOnPromotion = new[] { _itemPromotion.Id };
                    cart.AppliedPromotions.Add(new AppliedPromotion(promotion.Description, discount, _itemsOnPromotion, qtyPromotionPrice * promotion.NumberOfItems));
                }
            }
            return cart;
        }

        private bool ShouldApply(Cart cart, NItemsFixedPriceParameters promotion)
        {
            _itemPromotion = cart.CartItems.FirstOrDefault(x => x.Id == promotion.ItemId);
            if(_itemPromotion != null)
                _itemsAvailable = _itemPromotion.Quantity - cart.AppliedPromotions.Where(x => x.Items.Contains(_itemPromotion.Id)).Sum(s => s.Quantity);

            return _itemsAvailable >= promotion.NumberOfItems;
        }
    }
}
