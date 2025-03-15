using Microsoft.EntityFrameworkCore.Query;
using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T?> GetByIdAsync(int id);
		T? GetById(int id);
		IQueryable<T> GetAll();
		Task AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
		void Delete(int id);
		bool Exists(int id);
		void Attach(T entity);
		Task<IReadOnlyList<T>> ListAllAsync();
		Task<IEnumerable<T>> ListAsync();
		Task<IEnumerable<T>> ListAsync(
			Expression<Func<T, bool>>? filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null
		);

		Task<IEnumerable<T>> ListAsync(
			Expression<Func<T, bool>>? filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>>? includeProperties = null
		);
	}
}
