using Core.Domain;

namespace Core.Application
{
    public interface IOrderService
    {
        Task<bool> Buy(long userId, long productId);
        Task<List<Order>> ListAsync(int pageNumber, int pagesize);
    }
}