namespace Api.Models
{
    public class EnrolledBenefitRequest
    {
        public int employeeId { get; set; }
        public decimal standardEmployeeBenefitsCost { get; set; }
        public decimal dependentChildBenefitsCost { get; set; }
        public decimal agedDependentBenefitCost { get; set; }
        public decimal costlyResourceBenefitCost { get; set; }
        public decimal totalEnrolledBenefitCost { get; set; }
        public decimal totalBaseSalary { get; set; }
        public decimal totalBaseSalaryAfterDeduction{ get; set; }
        public decimal monthlyPayCheckSalaryAfterDeduction { get; set; }
    }

}
