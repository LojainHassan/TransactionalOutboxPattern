using TransactionalOutboxPattern.Models;

namespace TransactionalOutboxPattern.Contract;

public interface IOrderService
{
    Task<Order> AddOrder(Order order);
}
