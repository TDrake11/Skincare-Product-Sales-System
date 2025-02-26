using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.OrderService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

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

        [HttpGet("ListOrders")]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                var orders = await _orderService.GetAllOrderAsync();
                var orderModel = _mapper.Map<IEnumerable<OrderModel>>(orders);
                return Ok(orderModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }    
        }

        [HttpGet("getOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var orders = await _orderService.GetOrderByIdAsync(id);
                if (orders == null)
                    return NotFound();
                return Ok(_mapper.Map<OrderModel>(orders));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("createOrder")]
        public async Task<IActionResult> Create(OrderModel orderModel)
        {
            try
            {
                var orders = _mapper.Map<Order>(orderModel);

                await _orderService.AddOrderAsync(orders);
                return CreatedAtAction(nameof(GetOrderById), new { id = orders.Id }, _mapper.Map<OrderModel>(orders));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("updateOrder")]
        public async Task<IActionResult> Update([FromBody] OrderModel orderModel)
        {
            try
            {
                var order = _mapper.Map<Order>(orderModel);
                await _orderService.UpdateOrderAsync(order);
                return Ok("Order updated successfully.");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("deleteOrder/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return BadRequest("Order not found");
                }
                await _orderService.DeleteOrderAsync(id);
                return Ok("Order deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("listOrdersActive")]
        public async Task<IActionResult> GetActiveOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrderAsync();
                var deletedOrder = orders.Where(c => c.OrderStatus != OrderStatus.Completed.ToString());
                var orderModels = _mapper.Map<IEnumerable<OrderModel>>(deletedOrder);
                return Ok(orderModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("listOrderInactived")]
        public async Task<IActionResult> GetDeletedOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrderAsync();
                var deletedOrder = orders.Where(c => c.OrderStatus == OrderStatus.Completed.ToString());
                var orderModels = _mapper.Map<IEnumerable<OrderModel>>(deletedOrder);
                return Ok(orderModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
