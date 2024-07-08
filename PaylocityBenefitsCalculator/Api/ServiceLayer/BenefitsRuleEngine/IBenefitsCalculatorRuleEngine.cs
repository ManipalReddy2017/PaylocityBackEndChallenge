using Api.Dtos.Dependent;
using Api.Models;

namespace Api.ServiceLayer.BenefitRuleEngine
{
    public interface IBenefitsCalculatorRuleEngine
    {
        public EnrolledBenefitRequest CalculateEnrolledBenefits(ICollection<DependentDto> depedentList, decimal baseSalary);
    }
}
