using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.UI.Entities
{
    public class PercentageOfItemUnitPriceParameters
    {
        public char ItemId { get; set; }
        public decimal Percentage { get; set; }
        public string Description { get; set; }
    }
}
