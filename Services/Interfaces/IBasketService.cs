using Fiorello.ViewModels.BasketVM;

namespace Fiorello.Services.Interfaces
{
    public interface IBasketService
    {
        int BasketCount();
        List<BasketProductVM> GetProducts();
    }
}
