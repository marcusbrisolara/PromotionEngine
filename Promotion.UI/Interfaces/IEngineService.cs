using Promotion.UI.Entities;

namespace Promotion.UI.Interfaces
{
    public interface IEngineService
    {
        Cart ProcessPromotions(Cart cart);
    }
}
