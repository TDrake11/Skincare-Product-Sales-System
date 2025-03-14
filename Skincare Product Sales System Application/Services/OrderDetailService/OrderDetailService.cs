using Microsoft.EntityFrameworkCore;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.OrderDetailService
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailAsync()
        {
            return await _unitOfWork.Repository<OrderDetail>().ListAllAsync();
        }

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(int id)
        {
            return await _unitOfWork.Repository<OrderDetail>().GetByIdAsync(id);
        }

		public async Task<List<OrderDetail>> GetOrderDetailByOrderIdAsync(int orderId)
		{ 
			return await _unitOfWork.Repository<OrderDetail>()
                .GetAll()
                .Where(o => o.OrderId == orderId)
                .Include(o => o.Product)
                .ToListAsync();
		}
		public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            await _unitOfWork.Repository<OrderDetail>().AddAsync(orderDetail);
            await _unitOfWork.Complete();
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            _unitOfWork.Repository<OrderDetail>().Update(orderDetail);
            await _unitOfWork.Complete();
        }

        public async Task DeleteOrderDetailAsync(int id)
        {
            _unitOfWork.Repository<OrderDetail>().Delete(id);
            await _unitOfWork.Complete();
        }
    }
}
