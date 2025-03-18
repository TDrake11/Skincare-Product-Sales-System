using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
				registerModel.Email = registerModel.Email.ToLower();
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

				var user = await _userManager.GetUserAsync(User);
				userProfileUpdateModel.Email = userProfileUpdateModel.Email.ToLower();
				if (user == null)
				{
					return Unauthorized("User not authenticated");
				}

				// Kiểm tra nếu người dùng muốn thay đổi email
				if (!string.Equals(user.Email, userProfileUpdateModel.Email, StringComparison.OrdinalIgnoreCase))
				{
					var existingUser = await _userManager.FindByEmailAsync(userProfileUpdateModel.Email);
					if (existingUser != null)
					{
						return BadRequest("Email is already taken");
					}
					// Cập nhật email, username và normalized username
					user.Email = userProfileUpdateModel.Email;
					user.UserName = userProfileUpdateModel.Email;
					user.NormalizedUserName = userProfileUpdateModel.Email.ToUpper();
				}

				// Sử dụng AutoMapper để ánh xạ các thuộc tính khác
				_mapper.Map(userProfileUpdateModel, user);

				var result = await _userManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					return Ok("Profile updated successfully");
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

		[Authorize(Roles = "Admin")]
		[HttpGet("GetAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _userManager.Users.ToListAsync();
			var userProfiles = _mapper.Map<List<UserProfileModel>>(users);

			foreach (var userProfile in userProfiles)
			{
				var user = users.FirstOrDefault(u => u.Id == userProfile.Id);
				if (user != null)
				{
					var roles = await _userManager.GetRolesAsync(user);
					userProfile.RoleName = roles.FirstOrDefault(); // Lấy role đầu tiên nếu có nhiều role
				}
			}

			return Ok(userProfiles);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost("CreateStaffAccount")]
		public async Task<IActionResult> CreateStaffAccount([FromBody] RegisterModel registerModel)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				registerModel.Email = registerModel.Email.ToLower();
				if (await _userManager.FindByEmailAsync(registerModel.Email) != null)
				{
					return BadRequest("Email is already taken");
				}
				var user = _mapper.Map<User>(registerModel);
				user.UserName = registerModel.Email;
				user.Status = UserStatus.Actice.ToString();
				user.EmailConfirmed = true;
				var createdUser = await _userManager.CreateAsync(user, registerModel.Password);
				if (createdUser.Succeeded)
				{
					var roleResult = await _userManager.AddToRoleAsync(user, "Staff");
					if (roleResult.Succeeded)
					{
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

		[Authorize(Roles = "Admin")]
		[HttpPut("BanUser")]
		public async Task<IActionResult> BanUser(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return BadRequest("User not found");
			}
			user.Status = UserStatus.Ban.ToString();
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return Ok("User banned successfully");
			}
			else
			{
				return BadRequest(result.Errors);
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("ActiveUser")]
		public async Task<IActionResult> ActiveUser(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return BadRequest("User not found");
			}
			user.Status = UserStatus.Actice.ToString();
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return Ok("User activated successfully");
			}
			else
			{
				return BadRequest(result.Errors);
			}
		}
	}
}
