using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionalOutboxPattren.Contract;
using TransactionalOutboxPattren.Models;

namespace TransactionalOutboxPattren.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    [HttpPost]
    public async Task<IActionResult> Post(Order order)
    {
        var result = await _orderService.AddOrder(order);
        if (result is not null)
        {
            return Ok(result);
        }
        return BadRequest();
    }
}
