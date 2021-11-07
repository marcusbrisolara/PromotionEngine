using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.UI.Entities
{
    public class OneAndTwoFixedPriceParameters 
    {
        public char FirstItemId { get; set; }
        public char SecondItemId { get; set; }
        public decimal FixedPrice { get; set; }
        public string Description { get; set; }
    }
}
