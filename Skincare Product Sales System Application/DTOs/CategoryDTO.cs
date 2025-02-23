using Skincare_Product_Sales_System_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.DTOs
{
    public class CategoryDTO
    {
        [JsonIgnore] // Ẩn ID khi nhận request từ client
        public int? ID { get; set; }
        public string CategoryName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] // Chuyển CategoryStatus thành string khi serialize
        public CategoryStatus CategoryStatus { get; set; }
    }
}
