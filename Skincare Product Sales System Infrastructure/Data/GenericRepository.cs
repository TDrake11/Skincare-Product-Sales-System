using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Infrastructure.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly ApplicationDbContext _context;

		public GenericRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		public void Delete(int id)
		{
			var entity = _context.Set<T>().Find(id);
			if (entity != null)
			{
				_context.Set<T>().Remove(entity);
			}
		}

		public bool Exists(int id)
		{
			return _context.Set<T>().Any(e=>e.Id == id);
		}

		public IQueryable<T> GetAll()
		{
			return _context.Set<T>();
		}

		public T? GetById(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<IEnumerable<T>> ListAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
		{
			IQueryable<T> query = _context.Set<T>();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			return await query.ToListAsync(); ;
		}

		public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includeProperties = null)
		{
			IQueryable<T> query = _context.Set<T>();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (includeProperties != null)
			{
				query = includeProperties(query);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}
			return await query.ToListAsync(); 
		}

		public void Update(T entity)
		{
			_context.Set<T>().Update(entity);
		}

	}
}
