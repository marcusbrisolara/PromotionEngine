using Promotion.UI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.UI.Tests
{
    public static class TestData
    {
        internal static Cart ScenarioA_Cart = new Cart
        {
            CartItems = ScenarioA_CartItems
        };

        internal static Cart ScenarioB_Cart = new Cart
        {
            CartItems = ScenarioB_CartItems
        };

        internal static Cart ScenarioC_Cart = new Cart
        {
            CartItems = ScenarioC_CartItems
        };

        internal static Cart EmptyCart = new Cart();

        static List<CartItem> ScenarioA_CartItems => new List<CartItem>
        {
            new CartItem{ Id = 'A', Quantity = 1 , UnitPrice = 50 },
            new CartItem{ Id = 'B', Quantity = 1 , UnitPrice = 30 },
            new CartItem{ Id = 'C', Quantity = 1 , UnitPrice = 20 }
        };

        static List<CartItem> ScenarioB_CartItems => new List<CartItem>
        {
            new CartItem{ Id = 'A', Quantity = 5 , UnitPrice = 50 },
            new CartItem{ Id = 'B', Quantity = 5 , UnitPrice = 30 },
            new CartItem{ Id = 'C', Quantity = 1 , UnitPrice = 20 }
        };

        static List<CartItem> ScenarioC_CartItems => new List<CartItem>
        {
            new CartItem{ Id = 'A', Quantity = 3 , UnitPrice = 50 },
            new CartItem{ Id = 'B', Quantity = 5 , UnitPrice = 30 },
            new CartItem{ Id = 'C', Quantity = 1 , UnitPrice = 20 },
            new CartItem{ Id = 'D', Quantity = 1 , UnitPrice = 15 }
        };
    }
}
