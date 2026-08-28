using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionalOutboxPattern.Contract;
using TransactionalOutboxPattern.Contract.Email;
using TransactionalOutboxPattern.Models;

namespace TransactionalOutboxPattern.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMailService _mailService;
    public OrderController(IOrderService orderService, IMailService mailService)
    {
        _orderService = orderService;
        _mailService = mailService;
    }
    [HttpPost]
    public async Task<IActionResult> Post(Order order)
    {
        var result = await _orderService.AddOrder(order);
        if (result is not null)
        {
            // Send email if order store in the database
            var send = _mailService.Send(result.Email, "Order is completed", "Your order has been saved in the database", false);
            if (send is true)
            {
                return Ok(result);
            }
            else
            {
                // store in the email outbox             
            }
        }
        return BadRequest();
    }
}