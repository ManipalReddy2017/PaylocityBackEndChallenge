using Api.DataAccesLayer.Dependent;
using Api.DataAccesLayer.Employee;
using Api.DataAccesLayer.Model;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.ServiceLayer.BenefitRuleEngine;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Data;

namespace Api.ServiceLayer.Employee
{
    /// <summary>
    ///  Employee Service 
    /// </summary>
    public class EnrolledBenefitsService : IEnrolledBenefitsService
    {
        public IEnrolledBenefitsRepository enrolledBenefitsRepository;
        public EnrolledBenefitsService(IEnrolledBenefitsRepository enrolledBenefitsRepository)
        {
            this.enrolledBenefitsRepository = enrolledBenefitsRepository;
        }

        /// <summary>
        ///  GetEnrolledBenefitsId
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<EmployeeEnrolledBenefitDto?> GetEnrolledBenefitsId(int employeeId)
        {
            var employeeRequest = new EmployeeRequest()
            {
                EmployeeId = employeeId
            };
            var enrolledBenefitsDetails = await enrolledBenefitsRepository.GetEnrolledBenefitsId(employeeRequest);
            if (enrolledBenefitsDetails == null || enrolledBenefitsDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return GetEnrolledBenefits(enrolledBenefitsDetails)?.FirstOrDefault();
            }
        }

        /// <summary>
        /// GetEmployeeDetails
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        private static List<EmployeeEnrolledBenefitDto>? GetEnrolledBenefits(List<EnrolledBenefitsDetailResponse> enrolledBenefitsEmployes)
        {
            var employeeDtoList = enrolledBenefitsEmployes
                .GroupBy(u => u.EmployeeId)
                .Select(grp => grp.ToList())
                .Select(groupedEmployee => new EmployeeEnrolledBenefitDto()
                 {
                     EmployeeId = groupedEmployee.First().EmployeeId,
                     FirstName = groupedEmployee.First().FirstName,
                     LastName = groupedEmployee.First().LastName,
                     AnnualSalary = groupedEmployee.First().TotalBaseSalary,
                     DateOfBirth = groupedEmployee.First().E_DateOfBirth,
                     EnrolledBenefit = new EnrolledBenefitDto()
                                           {
                                                StandardEmployeeBenefitsCost = groupedEmployee.First().StandardEmployeeBenefitsCost,
                                                DependentChildBenefitsCost = groupedEmployee.First().DependentChildBenefitsCost,
                                                AgedDependentBenefitCost = groupedEmployee.First().AgedDependentBenefitCost,
                                                CostlyResourceBenefitCost = groupedEmployee.First().CostlyResourceBenefitCost,
                                                TotalBaseSalary = groupedEmployee.First().TotalBaseSalary,
                                                TotalEnrolledBenefitCost = groupedEmployee.First().TotalEnrolledBenefitCost,
                                                TotalBaseSalaryAfterDeduction = groupedEmployee.First().TotalBaseSalaryAfterDeduction,
                                                MonthlyPayCheckSalaryAfterDeduction = groupedEmployee.First().MonthlyPayCheckSalaryAfterDeduction
                                           },
                    Dependents = groupedEmployee.Count == 1 && groupedEmployee.First().DependentId == null ? null : groupedEmployee.Select(s => new DependentDto
                     {
                         FirstName = s.D_FirstName,
                         LastName = s.D_LastName,
                         DependentId = s.DependentId,
                         DateOfBirth = s.D_DateOfBirth,
                         Relationship = s.Relation != null ? (Relationship)s.Relation : Relationship.None,
                     }).ToList()
                 }).ToList();
            return employeeDtoList;
        }

        /// <summary>
        /// Get All Employee Details
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeeEnrolledBenefitDto>?> GetEnrolledBenefits()
        {
            var enrolledBenefitsDetails = await enrolledBenefitsRepository.GetEnrolledBenefitsAll();
            if (enrolledBenefitsDetails == null || enrolledBenefitsDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return GetEnrolledBenefits(enrolledBenefitsDetails)?.ToList();
            }
        }



    }
}
