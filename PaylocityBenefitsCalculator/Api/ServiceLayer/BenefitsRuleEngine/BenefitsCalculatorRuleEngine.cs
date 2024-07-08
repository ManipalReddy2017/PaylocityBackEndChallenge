using Api.Dtos.Dependent;
using Api.Models;
using System.Collections.Generic;
using System.Data;

namespace Api.ServiceLayer.BenefitRuleEngine
{
    /// <summary>
    /// BenefitsCalculatorRuleEngine
    /// </summary>
    public class BenefitsCalculatorRuleEngine : IBenefitsCalculatorRuleEngine
    {
        private decimal benefitCost = 0;
        private decimal baseSalary = 0;
        private const decimal baseStandardSalary = 80000;
        private const decimal employeedStandardBenefitsCost = 12000;
        private const decimal employeeChildBenefitsCost = 7200;
        private const decimal dependentAgeBenefits = 2400;
        private const decimal costlyResourcePercent = 2;
        private const int agedLimit = 50;
        private const decimal totalPayChecksPerYear = 26;

        public EnrolledBenefitRequest CalculateEnrolledBenefits(ICollection<DependentDto> depedentList, decimal baseSalary)
        {
            this.baseSalary = baseSalary;
            benefitCost = benefitCost + employeedStandardBenefitsCost;
            var dependentChildBenefitsCost = Calculate_DependentChildBenefitsCost(depedentList);
            var agedDependentBenefitCost = Calculate_AgedDependentBenefitCost(depedentList);
            var costlyResourceBenefitCost = Calculte_CostlyResourceBenefitCost();

            decimal totalEnrolledBenefitCost, totalBaseSalaryAfterDeduction;
            CaculateTotalBenefitsCosts(baseSalary, dependentChildBenefitsCost, costlyResourceBenefitCost, agedDependentBenefitCost, out totalEnrolledBenefitCost, out totalBaseSalaryAfterDeduction);

            return new EnrolledBenefitRequest()
            {
                totalBaseSalary = baseSalary,
                agedDependentBenefitCost = agedDependentBenefitCost,
                dependentChildBenefitsCost = dependentChildBenefitsCost,
                costlyResourceBenefitCost = costlyResourceBenefitCost,
                standardEmployeeBenefitsCost = employeedStandardBenefitsCost,
                totalEnrolledBenefitCost = totalEnrolledBenefitCost,
                totalBaseSalaryAfterDeduction = totalBaseSalaryAfterDeduction,
                monthlyPayCheckSalaryAfterDeduction = Calculate_MonthlyPayCheckSalaryAfterDeduction(totalBaseSalaryAfterDeduction)
            };
        }

        /// <summary>
        /// CaculateTotalBenefitsCosts
        /// </summary>
        /// <param name="baseSalary"></param>
        /// <param name="dependentChildBenefitsCost"></param>
        /// <param name="costlyResourceBenefitCost"></param>
        /// <param name="agedDependentBenefitCost"></param>
        /// <param name="totalEnrolledBenefitCost"></param>
        /// <param name="totalBaseSalaryAfterDeduction"></param>
        public static void CaculateTotalBenefitsCosts(decimal baseSalary, decimal dependentChildBenefitsCost, decimal costlyResourceBenefitCost, decimal agedDependentBenefitCost, out decimal totalEnrolledBenefitCost, out decimal totalBaseSalaryAfterDeduction)
        {
            totalEnrolledBenefitCost = employeedStandardBenefitsCost + dependentChildBenefitsCost + costlyResourceBenefitCost + agedDependentBenefitCost;
            totalBaseSalaryAfterDeduction = baseSalary - totalEnrolledBenefitCost;
        }

        /// <summary>
        /// Calculate Dependent Child Benefit Cost
        /// </summary>
        /// <param name="depedentList"></param>
        public decimal Calculate_DependentChildBenefitsCost(ICollection<DependentDto> depedentList)
        {
            var childDependents = depedentList.Where(s => s.Relationship == Relationship.Child).Count();
            return employeeChildBenefitsCost * childDependents;
        }

        /// <summary>
        ///  Calculate Aged Dependent Benefit Cost
        /// </summary>
        /// <param name="depedentList"></param>
        public decimal Calculate_AgedDependentBenefitCost(ICollection<DependentDto> depedentList)
        {
            var childDependents = depedentList.Where(s => DateTime.Now.Year - s.DateOfBirth.Year > agedLimit).Count();
            return dependentAgeBenefits * childDependents;
        }

        /// <summary>
        /// Calculate Costly Resource Benefit Cost
        /// </summary>
        public decimal Calculte_CostlyResourceBenefitCost()
        {
            if (baseSalary > baseStandardSalary)
            {
                return baseSalary * (costlyResourcePercent / 100);
            }
            return 0;
        }

        /// <summary>
        /// Calculate MonthlyPaycheckAfterBenefitDeduction
        /// </summary>
        /// <param name="totalBaseSalaryAfterDeduction"></param>
        /// <returns></returns>
        public decimal Calculate_MonthlyPayCheckSalaryAfterDeduction(decimal totalBaseSalaryAfterDeduction)
        {
            return totalBaseSalaryAfterDeduction / totalPayChecksPerYear;
        }
    }
}
