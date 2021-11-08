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
        private char[] _itemsOnPromotion;
        private int _qtyAvailableFirstItem;
        private int _qtyAvailableSecondItem;
        private CartItem _firstItemPromotion;
        private CartItem _secondItemPromotion;

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
                    var qtyPromotionPrice = Math.Min(_qtyAvailableFirstItem, _qtyAvailableSecondItem);

                    var discount =
                        (qtyPromotionPrice * _firstItemPromotion.UnitPrice + qtyPromotionPrice * _secondItemPromotion.UnitPrice) - qtyPromotionPrice * promotion.FixedPrice;
                    
                    _itemsOnPromotion = new[] { _firstItemPromotion.Id, _secondItemPromotion.Id };
                    
                    cart.AppliedPromotions.Add(new AppliedPromotion(promotion.Description, discount, _itemsOnPromotion, qtyPromotionPrice));
                }
            }
            return cart;
        }

        private bool ShouldApply(Cart cart, OneAndTwoFixedPriceParameters promotion)
        {
            _firstItemPromotion = cart.CartItems.FirstOrDefault(x => x.Id == promotion.FirstItemId);
            if (_firstItemPromotion != null)
                _qtyAvailableFirstItem = _firstItemPromotion.Quantity - cart.AppliedPromotions.Where(x => x.Items.Contains(_firstItemPromotion.Id)).Sum(s => s.Quantity);
            
            _secondItemPromotion = cart.CartItems.FirstOrDefault(x => x.Id == promotion.SecondItemId);            
            if(_secondItemPromotion != null)
                _qtyAvailableSecondItem = _secondItemPromotion.Quantity - cart.AppliedPromotions.Where(x => x.Items.Contains(_secondItemPromotion.Id)).Sum(s => s.Quantity);

            return (_qtyAvailableFirstItem > 0 && _qtyAvailableSecondItem > 0);
                
        }
    }
}
