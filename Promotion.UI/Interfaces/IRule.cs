using Promotion.UI.Entities;

namespace Promotion.UI.Interfaces
{
    public interface IRule
    {
        Cart Apply(Cart cart);
    }
}
