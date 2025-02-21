using Skincare_Product_Sales_System_Application.Interfaces;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(T entity)
        {
            await _repository.Add(entity);
        }

        public async Task Update(T entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
