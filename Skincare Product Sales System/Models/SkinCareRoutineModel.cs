using Skincare_Product_Sales_System_Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skincare_Product_Sales_System.Models
{
    public class SkinCareRoutineModel
    {
        public int Id { get; set; }
        public string RoutineName { get; set; }
        public string Description { get; set; }
        public int TotalSteps { get; set; }
        public int? SkinTypeId { get; set; }
    }
}
