using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.UI.Entities
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<AppliedPromotion> AppliedPromotions { get; set; } = new List<AppliedPromotion>();
        public decimal OriginalTotalPrice => CartItems?.Sum(x => x.TotalPrice) ?? 0;
        public decimal FinalTotalPrice => OriginalTotalPrice - (AppliedPromotions.Sum(x => x.Discount));
    }
}
