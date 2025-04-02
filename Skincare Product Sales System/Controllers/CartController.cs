using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.OrderDetailService;
using Skincare_Product_Sales_System_Application.Services.OrderService;
using Skincare_Product_Sales_System_Application.Services.ProductService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using System.Security.Claims;

namespace Skincare_Product_Sales_System.Controllers
{
	[Authorize(Roles ="Customer")]
	[Route("api/cart")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly IOrderDetailService _orderDetailService;
		private readonly IProductService _productService;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		public CartController(IOrderService orderService, IOrderDetailService orderDetailService, IProductService productService , IMapper mapper, UserManager<User> userManager)
		{
			_orderService = orderService;
			_orderDetailService = orderDetailService;
			_productService = productService;
			_mapper = mapper;
			_userManager = userManager;
		}

		[HttpGet("GetCart")]
		public async Task<IActionResult> GetCartByUse()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				Console.WriteLine("User is null. Authentication might have failed.");
				return Unauthorized("User not authenticated.");
			}
			var order = _orderService.GetCartByUserAsync(user);
			if(order == null)
			{
				order = new Order
				{
					CustomerId = user.Id,
					OrderDate = DateTime.Now,
					OrderStatus = OrderStatus.Cart.ToString()
				};
				await _orderService.AddOrderAsync(order);
				return Ok(order);

			}
			var cartModel = _mapper.Map<CartModel>(order);
			var cartDetails = await _orderDetailService.GetOrderDetailByOrderIdAsync(order.Id);
			var cartDetailModels = _mapper.Map<List<CartDetailModel>>(cartDetails);
			var baseUrl = $"{Request.Scheme}://{Request.Host}";
			foreach (var orderDetail in cartDetailModels)
			{
				if (!string.IsNullOrEmpty(orderDetail.Image))
				{
					orderDetail.Image = $"{baseUrl}{orderDetail.Image}";
				}
			}
			cartModel.ListOrderDetail = cartDetailModels;
			return Ok(cartModel);
		}

		[HttpPost("AddProductIntoCart")]
		public async Task<IActionResult> AddProductIntoCart(int productId, int quantity)
		{
			var user = await _userManager.GetUserAsync(User);
			var order = _orderService.GetCartByUserAsync(user);
			if (order == null) 
			{
				order = new Order
				{
					CustomerId = user.Id,
					OrderStatus = OrderStatus.Cart.ToString()
				};
				await _orderService.AddOrderAsync(order);
			}
			var orderDetails =  await _orderDetailService.GetOrderDetailByOrderIdAsync(order.Id);
			var product =  _productService.GetProductById(productId);
			if (orderDetails.Any(o => o.ProductId == productId))
			{
				var orderDetail = orderDetails.FirstOrDefault(o => o.ProductId == productId);
				orderDetail.Quantity += quantity;
				await _orderDetailService.UpdateOrderDetailAsync(orderDetail);
			}
			else
			{
				var orderDetail = new OrderDetail
				{
					OrderId = order.Id,
					ProductId = productId,
					Quantity = quantity,
					Price = product.Price
				};
				await _orderDetailService.AddOrderDetailAsync(orderDetail);
			}
			order.TotalPrice += product.Price * quantity;
			await _orderService.UpdateOrderAsync(order);
			return Ok();
		}



		[HttpDelete("RemoveProductFromCart")]
		public async Task<IActionResult> RemoveProductFromCart(int orderDetailId)
		{
			var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(orderDetailId);
			if (orderDetail == null)
			{
				return BadRequest("Order detail not found");
			}
			var order = await _orderService.GetOrderByIdAsync(orderDetail.OrderId);
			if (order == null)
			{
				return BadRequest("Order not found");
			}
			order.TotalPrice -= orderDetail.Price * orderDetail.Quantity;
			await _orderService.UpdateOrderAsync(order);
			await _orderDetailService.DeleteOrderDetailAsync(orderDetailId);
			return Ok();
		}

		[HttpPut("UpdateQuantity")]
		public async Task<IActionResult> UpdateQuantity(int orderDetailId, int quantity)
		{
			var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(orderDetailId);
			if (orderDetail == null)
			{
				return BadRequest("Order detail not found");
			}
			var order = await _orderService.GetOrderByIdAsync(orderDetail.OrderId);
			if (order == null)
			{
				return BadRequest("Order not found");
			}
			order.TotalPrice += orderDetail.Price * (quantity - orderDetail.Quantity);
			orderDetail.Quantity = quantity;
			order.TotalPrice += orderDetail.Price * orderDetail.Quantity;
			await _orderService.UpdateOrderAsync(order);
			await _orderDetailService.UpdateOrderDetailAsync(orderDetail);
			return Ok();
		}


		[HttpPut("Checkout")]
		public async Task<IActionResult> Checkout(List<int> orderDetailsId, double totalPrice)
		{
			var user = await _userManager.GetUserAsync(User);
			var cart = _orderService.GetCartByUserAsync(user);
			if(totalPrice > user.Wallet)
			{
				return BadRequest("Not enough money in wallet");
			}
			var order = new Order
			{
				CustomerId = user.Id,
				OrderDate = DateTime.Now,
				OrderStatus = OrderStatus.Pending.ToString(),
				TotalPrice = totalPrice
			};
			foreach (var id in orderDetailsId)
			{
				var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(id);
				if (orderDetail == null)
				{
					return BadRequest("Order detail not found");
				}
				var product = _productService.GetProductById(orderDetail.ProductId);
				if (product.Quantity < orderDetail.Quantity)
				{
					return BadRequest("Out of stock");
				}
				product.Quantity -= orderDetail.Quantity;
				orderDetail.OrderId = order.Id;
				orderDetail.Order = order;
				await _orderDetailService.UpdateOrderDetailAsync(orderDetail);
			}
			cart.TotalPrice -= totalPrice;
			user.Wallet -= totalPrice;
			user.Point += (int)(totalPrice * 0.01);
			await _userManager.UpdateAsync(user);
			await _orderService.UpdateOrderAsync(cart);
			return Ok();
		}

		[HttpPut("CanceledOrder")]
		public async Task<IActionResult> CanceledOrder(int orderId, double totalPrice, string status)
		{
			var order = await _orderService.GetOrderByIdAsync(orderId);
			var listOrderDetail = await _orderDetailService.GetOrderDetailByOrderIdAsync(orderId);
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized("User not authenticated");
			}
			if (order == null)
			{
				return BadRequest("Order not found");
			}
			if (order.OrderStatus != OrderStatus.Pending.ToString())
			{
				return BadRequest("Order can not be canceled");
			}
			foreach (var orderDetail in listOrderDetail) 
			{
				var product = _productService.GetProductById(orderDetail.ProductId);
				product.Quantity += orderDetail.Quantity;
			}
			order.OrderStatus = OrderStatus.Canceled.ToString();
			user.Wallet += totalPrice;
			user.Point -= (int)(totalPrice*0.01);
			await _orderService.UpdateOrderAsync(order);
			return Ok();
		}

	}
}
