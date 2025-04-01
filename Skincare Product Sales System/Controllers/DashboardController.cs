using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System_Application.Services.OrderService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
	//[Authorize(Roles = "Admin")]
	//[ApiController]
	[Route("api/Dashboard")]
	public class DashboardController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly UserManager<User> _userManager;
		public DashboardController(IOrderService orderService, UserManager<User> userManager)
		{
			_orderService = orderService;
			_userManager = userManager;
		}

		[HttpGet("TotalDashboard")]
		public async Task<IActionResult> TotalDashboard()
		{
			var listCustomer = await _userManager.GetUsersInRoleAsync("Customer");
			int totalCustomer = listCustomer.Count;
			var listOrder = await _orderService.GetAllOrderAsync();
			int totalOrder = 0;
			double totalRevenue = 0;
			foreach (var order in listOrder)
			{
				if(order.OrderStatus.Equals(OrderStatus.Completed.ToString()) && order.OrderDate.Year == DateTime.Now.Year)
				{
					totalRevenue += order.TotalPrice;
					totalOrder++;
				}
			}
			return Ok(new { totalRevenue, totalOrder, totalCustomer });
		}

		[HttpGet("DashboardOrder")]
		public async Task<IActionResult> Dashboard()
		{
			var listOrder = await _orderService.GetAllOrderAsync();
			List<Double> dashboard = new List<Double>();
			for(int i = 0; i < 12; i++)
			{
				double totalPrice = 0;
				foreach (var order in listOrder)
				{
					if(order.OrderDate.Month == i + 1 && order.OrderDate.Year == DateTime.Now.Year && order.OrderStatus.Equals(OrderStatus.Completed.ToString()))
					{
						totalPrice += order.TotalPrice;
					}
				}
				dashboard.Add(totalPrice);
			}
			
			return Ok(dashboard);
		}

	}
}
