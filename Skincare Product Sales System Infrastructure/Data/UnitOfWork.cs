using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Infrastructure.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		private readonly IServiceProvider _serviceProvider;
		private Dictionary<Type, object> _repositories;
		public UnitOfWork(ApplicationDbContext context, IServiceProvider serviceProvider)
		{
			_context = context;
			_serviceProvider = serviceProvider;
		}

		//public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		//{
		//	if(_repositories == null)
		//	{
		//		_repositories = new Dictionary<Type, object>();
		//	}
		//	var entityType = typeof(TEntity);

		//	if (!_repositories.TryGetValue(entityType, out var repository))
		//	{
		//		var repositoryInstance = Activator.CreateInstance(
		//			typeof(GenericRepository<>).MakeGenericType(entityType),
		//			_context
		//		);
		//		_repositories[entityType] = repositoryInstance!;
		//	}

		//	return (IGenericRepository<TEntity>)_repositories[entityType]!;
		//}

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			return _serviceProvider.GetRequiredService<IGenericRepository<TEntity>>();
		}

		public async Task<int> Complete()
		{
			try
			{
				return await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				// Kiểm tra lỗi nội bộ từ database
				var innerMessage = ex.InnerException?.Message ?? "No inner exception";
				Console.WriteLine($"DbUpdateException: {ex.Message}");
				Console.WriteLine($"Inner Exception: {innerMessage}");

				// Nếu cần, có thể log lỗi vào file hoặc hệ thống giám sát
				throw; // Ném lại lỗi để debug dễ hơn
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Unexpected Error: {ex.Message}");
				throw;
			}
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
