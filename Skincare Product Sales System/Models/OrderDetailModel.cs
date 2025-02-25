﻿namespace Skincare_Product_Sales_System.Models
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
    }
}
