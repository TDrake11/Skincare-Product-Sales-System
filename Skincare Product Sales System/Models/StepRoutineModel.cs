namespace Skincare_Product_Sales_System.Models
{
    public class StepRoutineModel
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string? StepDescription { get; set; }
        public string? Status { get; set; }
        public int RoutineId { get; set; }
        public string? RoutineName { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
    }

    public class CreateStepRoutineModel
    {
        public int StepNumber { get; set; }
        public string? StepDescription { get; set; }
        public int RoutineId { get; set; }
        public int ProductId { get; set; }
    }

    public class UpdateStepRoutineModel 
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string? StepDescription { get; set; }
        public int RoutineId { get; set; }
        public int ProductId { get; set; }
    }
}
