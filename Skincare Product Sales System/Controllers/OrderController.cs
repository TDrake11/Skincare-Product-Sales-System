using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public OrderController(IOrderService orderService, IMapper mapper, UserManager<User> userManager)
        {
            _orderService = orderService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("ListOrders")]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                var orders = await _orderService.GetAllOrderAsync();
                var orderModel = _mapper.Map<IEnumerable<OrderModel>>(orders);
                foreach (var order in orderModel)
                {
                    var user = await _userManager.FindByIdAsync(order.CustomerId);
                    var staff = await _userManager.FindByIdAsync(order.StaffId);
                    if(staff != null)
                    {
						order.StaffName = staff.FirstName + " " + staff.LastName;
					}
                    order.CustomerName = user.FirstName + " " + user.LastName;
                    
                }
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
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return Ok("No order found.");
                }
                    
                return Ok(_mapper.Map<OrderModel>(order));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateOrder")]
        public async Task<IActionResult> Update([FromBody] UpdateOrderModel orderModel)
        {
            try
            {
                var existingOrder = await _orderService.GetOrderByIdAsync(orderModel.Id);
                if (existingOrder == null)
                {
                    return NotFound("Order not found.");
                }

                existingOrder.OrderStatus = orderModel.OrderStatus;
                existingOrder.StaffId = orderModel.StaffId;

                await _orderService.UpdateOrderAsync(existingOrder);
                return Ok("Order updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpDelete("deleteOrder/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return Ok("Order not found");
                }
            await _orderService.DeleteOrderAsync(id);
                return Ok("Order deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
