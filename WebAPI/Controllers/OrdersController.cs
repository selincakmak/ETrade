using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;
        private IOrderLineService _orderLineService;


        public OrdersController(IOrderService orderService, IOrderLineService orderLineService)
        {
            _orderService = orderService;
            _orderLineService = orderLineService;

        }
        [HttpPost(template: "add")]

        public IActionResult Add(Order order)
        {
            var result = _orderService.Add(order);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost(template: "addOrderLine")]
        public IActionResult Add(OrderLine orderLine)
        {
            var result = _orderLineService.Add(orderLine);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
