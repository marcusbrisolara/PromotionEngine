using System.Collections.Generic;

namespace Promotion.UI.Entities
{
    public class AppliedPromotion
    {
        public AppliedPromotion(string description, decimal discount, char[] Items, int quantity)
        {
            this.Description = description;
            this.Discount = discount;
            this.Items = Items;
            this.Quantity = quantity;
        }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public char[] Items { get; set; }
        public int Quantity { get; set; }
    }
}
