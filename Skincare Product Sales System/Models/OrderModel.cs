﻿namespace Skincare_Product_Sales_System.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public string? CustomerId { get; set; }
        public string? StaffId { get; set; }
    }
}
