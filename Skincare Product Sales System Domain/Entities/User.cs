﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public class User : IdentityUser
	{
		[MaxLength(100)]
		public string? FirstName { get; set; }

		[MaxLength(100)]
		public string? LastName { get; set; }
		public string? Address { get; set; }
		public DateOnly? Birthday { get; set; }
		public string? Avatar { get; set; }
		public double? Wallet { get; set; } = 0;
		public int? Point { get; set; } = 0;
		public string? Status { get; set; }

		//public virtual ICollection<Order>? Orders { get; set; } //  1 khach hang co the co nhieu order

		//public virtual ICollection<Order>? ProcessedOrders { get; set; } //  1 nhan vien co the xu ly nhieu don hang

		public virtual ICollection<Comment>? Comments { get; set; } // 1 user co nhieu comment

		public virtual ICollection<Product>? Products { get; set; } // 1 nhan vien co the quan li nhieu product

		public virtual ICollection<SkinTest>? SkinTests { get; set; } // 1 user co the co nhieu skin test
	}
}
