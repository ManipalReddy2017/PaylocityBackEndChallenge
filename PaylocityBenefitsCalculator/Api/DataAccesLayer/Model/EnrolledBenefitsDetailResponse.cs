using Api.Models;

namespace Api.DataAccesLayer.Model
{
    public class EnrolledBenefitsDetailResponse
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? DependentId { get; set; }
        public string? D_FirstName { get; set; }
        public string? D_LastName { get; set; }
        public DateTime E_DateOfBirth { get; set; }
        public DateTime D_DateOfBirth { get; set; }
        public int? Relation { get; set; }
        public decimal StandardEmployeeBenefitsCost { get; set; }
        public decimal AgedDependentBenefitCost { get; set; }
        public decimal CostlyResourceBenefitCost { get; set; }
        public decimal TotalEnrolledBenefitCost { get; set; }
        public decimal TotalBaseSalary { get; set; }
        public decimal TotalBaseSalaryAfterDeduction { get; set; }
        public decimal MonthlyPayCheckSalaryAfterDeduction { get; set; }
        public decimal DependentChildBenefitsCost { get; set; }
    }
}
