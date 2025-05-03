

using Order.Application.DTO;

namespace Order.Application.Interfaces
{
    public interface IProductService
    {
        Task<bool> UpdateInventory(int productId, int quantity);
    }
}
