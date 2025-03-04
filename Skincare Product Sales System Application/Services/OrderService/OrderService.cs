using Microsoft.AspNetCore.Identity;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await _unitOfWork.Repository<Order>().ListAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Order>().GetByIdAsync(id);
        }
        public async Task<Order?> GetCartByUserAsync(User user)
		{
			var cart =  _unitOfWork.Repository<Order>()
                .GetAll()
                .Where(o => o.Customer == user && o.OrderStatus == OrderStatus.Cart.ToString())
                .FirstOrDefault();
            return cart;
		}
		public async Task AddOrderAsync(Order order)
        {
            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.Complete();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.Complete();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(id);
            if (order != null)
            {
                order.OrderStatus = OrderStatus.Cancelled.ToString();
                _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.Complete();
        }
    }
}
}
