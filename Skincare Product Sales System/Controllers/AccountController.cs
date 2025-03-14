using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.OrderService;
using Skincare_Product_Sales_System_Application.Services.TokenService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
	[ApiController]
	[Route("api/account")]
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly IOrderService _orderService;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;
		public AccountController(UserManager<User> userManager, IMapper mapper, ITokenService tokenService, IOrderService orderService)
		{
			_userManager = userManager;
			_mapper = mapper;
			_tokenService = tokenService;
			_orderService = orderService;
		}

		//[HttpPost("login")]
		//public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
		//{
		//	try
		//	{
		//		if (!ModelState.IsValid)
		//		{
		//			return BadRequest(ModelState);
		//		}
		//		var user = await _userManager.FindByEmailAsync(loginModel.Email);
		//		if (user == null)
		//		{
		//			return BadRequest("Invalid email");
		//		}
		//		var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
		//		if (result)
		//		{
		//			return Ok(
		//				new UserTokenModel
		//				{
		//					UserName = user.UserName,
		//					Email = user.Email,
		//					Token = _tokenService.CreateToken(user)
		//				}
		//			);
		//		}
		//		else
		//		{
		//			return BadRequest("User not found and/or password incorrect");
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		return BadRequest(ex.Message);
		//	}
		//}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
		{
			try
			{
				if(!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				if(await _userManager.FindByEmailAsync(registerModel.Email) != null)
				{
					return BadRequest("Email is already taken");
				}
				var user = _mapper.Map<User>(registerModel);
				user.UserName = registerModel.Email;
				user.Status = UserStatus.Actice.ToString();
				user.EmailConfirmed = true;
				var createdUser = await _userManager.CreateAsync(user, registerModel.Password);
				if(createdUser.Succeeded)
				{
					var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
					if(roleResult.Succeeded)
					{
						var order = new Order
						{
							CustomerId = user.Id,
							OrderDate = DateTime.Now,
							OrderStatus = OrderStatus.Cart.ToString()
						};
						await _orderService.AddOrderAsync(order);
						return Ok();
					}
					else
					{
						return BadRequest(roleResult.Errors);
					}
				}
				else
				{
					return BadRequest(createdUser.Errors);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize]
		[HttpGet("GetUserProfile")]
		public async Task<IActionResult> GetUserProfile()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized("User not authenticated");
			}
			var role = await _userManager.GetRolesAsync(user);
			var userProfile = _mapper.Map<UserProfileModel>(user);
			userProfile.RoleName = role.FirstOrDefault();
			return Ok(userProfile);
		}

		[Authorize]
		[HttpPut("UpdateUserProfile")]
		public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileUpdateModel userProfileUpdateModel)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				if (await _userManager.FindByEmailAsync(userProfileUpdateModel.Email) != null)
				{
					return BadRequest("Email is already taken");
				}
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
				{
					return Unauthorized("User not authenticated");
				}
				_mapper.Map(userProfileUpdateModel, user);
				var result = await _userManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					return Ok();
				}
				else
				{
					return BadRequest(result.Errors);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
