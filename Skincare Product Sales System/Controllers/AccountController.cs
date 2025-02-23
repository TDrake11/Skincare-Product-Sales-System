using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		public AccountController(UserManager<User> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}
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
				var createdUser = await _userManager.CreateAsync(user, registerModel.Password);
				if(createdUser.Succeeded)
				{
					var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
					if(roleResult.Succeeded)
					{
						return Ok("Register successfully");
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
	}
}
