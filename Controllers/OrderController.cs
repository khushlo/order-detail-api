using Microsoft.AspNetCore.Mvc;
using OrderInquiry.Handlers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderInquiry.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orderManagement = new OrderHandler();
            var response = await orderManagement.GetAllOrdersAsync(new CancellationToken());
            if (response.Count() == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("{customerID}/{startDate?}/{endDate?}")]
        public async Task<IActionResult> Get(int customerID, DateTime? startDate, DateTime? endDate)
        {
            var orderManagement = new OrderHandler();
            var response = await orderManagement.GetOrdersAsync(customerID,startDate, endDate, new CancellationToken());
            if (response.Count() == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }

    }
}
