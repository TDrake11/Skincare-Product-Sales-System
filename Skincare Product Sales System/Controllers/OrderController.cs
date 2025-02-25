using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.OrderService;
using Skincare_Product_Sales_System_Domain.Entities;

namespace Skincare_Product_Sales_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var orders = await _orderService.GetAllOrderAsync();
            var orderModel = _mapper.Map<IEnumerable<OrderModel>>(orders);
            return Ok(orderModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var orders = await _orderService.GetOrderByIdAsync(id);
            if (orders == null)
                return NotFound();
            return Ok(_mapper.Map<OrderModel>(orders));
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderModel orderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orders = _mapper.Map<Order>(orderModel);

            await _orderService.AddOrderAsync(orders);
            return CreatedAtAction(nameof(GetOrderById), new { id = orders.Id }, _mapper.Map<OrderModel>(orders));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderModel orderModel)
        {
            if (id != orderModel.Id)
                return BadRequest();

            var order = _mapper.Map<Order>(orderModel);
            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
