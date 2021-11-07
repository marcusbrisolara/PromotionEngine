using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.UI.Entities
{
    public class AppliedPromotion
    {
        public AppliedPromotion(string description, decimal discount)
        {
            this.Description = description;
            this.Discount = discount;
        }
        public string Description { get; set; }
        public decimal Discount { get; set; }
    }
}
