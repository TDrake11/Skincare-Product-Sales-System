﻿using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<T> Repository<T>() where T : BaseEntity;
		Task<int> Complete();
	}
}
