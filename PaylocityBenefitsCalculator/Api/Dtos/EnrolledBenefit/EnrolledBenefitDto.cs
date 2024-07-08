namespace Api.Models
{
    public class EnrolledBenefitDto
    {
        public decimal StandardEmployeeBenefitsCost { get; set; }
        public decimal DependentChildBenefitsCost { get; set; }
        public decimal AgedDependentBenefitCost { get; set; }
        public decimal CostlyResourceBenefitCost { get; set; }
        public decimal TotalEnrolledBenefitCost { get; set; }
        public decimal TotalBaseSalary { get; set; }
        public decimal TotalBaseSalaryAfterDeduction{ get; set; }
        public decimal MonthlyPayCheckSalaryAfterDeduction { get; set; }
    }

}
