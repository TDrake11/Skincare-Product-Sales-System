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

        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetail()
        {
            var orderDetails = await _orderDetailService.GetAllOrderDetailAsync();
            var orderDetailModel = _mapper.Map<IEnumerable<OrderDetailModel>>(orderDetails);
            return Ok(orderDetailModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var orderDetails = await _orderDetailService.GetOrderDetailByIdAsync(id);
            if (orderDetails == null)
                return NotFound();
            return Ok(_mapper.Map<OrderDetailModel>(orderDetails));
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDetailModel orderDetailModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderDetails = _mapper.Map<OrderDetail>(orderDetailModel);

            await _orderDetailService.AddOrderDetailAsync(orderDetails);
            return CreatedAtAction(nameof(GetOrderDetailById), new { id = orderDetails.Id }, _mapper.Map<OrderDetailModel>(orderDetails));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderDetailModel orderDetailModel)
        {
            if (id != orderDetailModel.Id)
                return BadRequest();

            var orderDetail = _mapper.Map<OrderDetail>(orderDetailModel);
            await _orderDetailService.UpdateOrderDetailAsync(orderDetail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderDetailService.DeleteOrderDetailAsync(id);
            return NoContent();
        }
    }
}
