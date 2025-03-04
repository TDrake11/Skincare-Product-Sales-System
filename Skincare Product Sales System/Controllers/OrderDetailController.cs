using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.OrderDetailService;
using Skincare_Product_Sales_System_Domain.Entities;

namespace Skincare_Product_Sales_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailService orderDetailService, IMapper mapper)
        {
            _orderDetailService = orderDetailService;
            _mapper = mapper;
        }

        [HttpGet("ListOrderDetails")]
        public async Task<IActionResult> GetAllOrderDetail()
        {
            try
            {
            var orderDetails = await _orderDetailService.GetAllOrderDetailAsync();
            var orderDetailModel = _mapper.Map<IEnumerable<OrderDetailModel>>(orderDetails);
            return Ok(orderDetailModel);
        }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("getOrderDetailById/{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            try
            {
            var orderDetails = await _orderDetailService.GetOrderDetailByIdAsync(id);
            if (orderDetails == null)
                return NotFound();
            return Ok(_mapper.Map<OrderDetailModel>(orderDetails));
        }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("createOrderDetail")]
        public async Task<IActionResult> Create(OrderDetailModel orderDetailModel)
        {

            var orderDetails = _mapper.Map<OrderDetail>(orderDetailModel);

            await _orderDetailService.AddOrderDetailAsync(orderDetails);
            return CreatedAtAction(nameof(GetOrderDetailById), new { id = orderDetails.Id }, _mapper.Map<OrderDetailModel>(orderDetails));
        }

        [HttpPut("updateOrderDetail")]
        public async Task<IActionResult> Update([FromBody] OrderDetailModel orderDetailModel)
        {
            try
            {
                var orderDetail = _mapper.Map<OrderDetail>(orderDetailModel);
                await _orderDetailService.UpdateOrderDetailAsync(orderDetail);
                return Ok("OrderDetail updated successfully.");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("deleteOrderDetail/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var orderDt = await _orderDetailService.GetOrderDetailByIdAsync(id);
                if (orderDt == null)
                {
                    return BadRequest("OrderDetail not found");
                }
            await _orderDetailService.DeleteOrderDetailAsync(id);
                return Ok("OrderDetail deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
