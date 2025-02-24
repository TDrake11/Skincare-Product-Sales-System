﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.TokenService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;
		public AccountController(UserManager<User> userManager, IMapper mapper, ITokenService tokenService)
		{
			_userManager = userManager;
			_mapper = mapper;
			_tokenService = tokenService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				var user = await _userManager.FindByEmailAsync(loginModel.Email);
				if (user == null)
				{
					return BadRequest("Invalid email");
				}
				var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
				if (result)
				{
					return Ok(
						new UserTokenModel
						{
							UserName = user.UserName,
							Email = user.Email,
							Token = _tokenService.CreateToken(user)
						}
					);
				}
				else
				{
					return BadRequest("User not found and/or password incorrect");
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
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
				user.EmailConfirmed = true;
				var createdUser = await _userManager.CreateAsync(user, registerModel.Password);
				if(createdUser.Succeeded)
				{
					var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
					if(roleResult.Succeeded)
					{
						return Ok(
							new UserTokenModel
							{
								UserName = user.UserName,
								Email = user.Email,
								Token = _tokenService.CreateToken(user)
							}
						);
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
