using Microsoft.EntityFrameworkCore;
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
		private Dictionary<Type, object> _repositories;
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;	
		}

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			if(_repositories == null)
			{
				_repositories = new Dictionary<Type, object>();
			}
			var entityType = typeof(TEntity);

			if (!_repositories.TryGetValue(entityType, out var repository))
			{
				var repositoryInstance = Activator.CreateInstance(
					typeof(GenericRepository<>).MakeGenericType(entityType),
					_context
				);
				_repositories[entityType] = repositoryInstance!;
			}

			return (IGenericRepository<TEntity>)_repositories[entityType]!;
		}

		public async Task<int> Complete()
		{
			try
			{
				return await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new Exception($"Database update error: {ex.InnerException?.Message}");
			}
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
