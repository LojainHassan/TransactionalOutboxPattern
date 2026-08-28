using TransactionalOutboxPattren.Models;

namespace TransactionalOutboxPattren.Contract;

public interface IOrderService
{
    Task<Order> AddOrder(Order order);
}
