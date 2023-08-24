using Core.Application;
using Microsoft.AspNetCore.Mvc;

namespace SnappFood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateNew")]
        public async Task<IActionResult> CreateNew(long userId, long productId)
        {
            return Ok(new { result = await _orderService.Buy(userId,  productId) });
        }


        [HttpGet("List")]
        public async Task<IActionResult> ListAsync(int pageNumber, int pagesize)
        {
            return Ok(await _orderService.ListAsync(pageNumber, pagesize));
        }
    }
}
