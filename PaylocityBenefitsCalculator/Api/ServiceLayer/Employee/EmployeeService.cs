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
    public class EmployeeService : IEmployeeService
    {
        public IEmployeeRepository employeeRepository;
        public IDependentRepository dependentRepository;
        public IBenefitsCalculatorRuleEngine benefitCalculatorRuleEngine;
        public EmployeeService(IEmployeeRepository employeeRepository,
                                IDependentRepository depedentRepository,
                                IBenefitsCalculatorRuleEngine benefitCalculatorRuleEngine
                            )
        {
            this.employeeRepository = employeeRepository;
            dependentRepository = depedentRepository;
            this.benefitCalculatorRuleEngine = benefitCalculatorRuleEngine;
        }

        /// <summary>
        ///  GetEmployeeById
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<EmployeeDto?> GetEmployeeById(int employeeId)
        {
            var employeeRequest = new EmployeeRequest()
            {
                EmployeeId = employeeId
            };
            var employees = await employeeRepository.GetEmployeeById(employeeRequest);
            if (employees == null || employees.Count == 0)
            {
                return null;
            }
            else
            {
                return GetEmployeeDetails(employees)?.FirstOrDefault();
            }
        }


        /// <summary>
        /// GetEmployeeDetails
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        private static List<EmployeeDto>? GetEmployeeDetails(List<EmployeeDetailResponse> employees)
        {
            var employeeDtoList = employees
                .GroupBy(u => u.EmployeeId)
                .Select(grp => grp.ToList())
                .Select(groupedEmployee => new EmployeeDto()
                 {
                     EmployeeId = groupedEmployee.First().EmployeeId,
                     FirstName = groupedEmployee.First().FirstName,
                     LastName = groupedEmployee.First().LastName,
                     AnnualSalary = groupedEmployee.First().TotalBaseSalary,
                     DateOfBirth = groupedEmployee.First().E_DateOfBirth,
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
        public async Task<List<EmployeeDto>?> GetEmployeeAll()
        {
            var employees = await employeeRepository.GetEmployeesAll();
            if (employees == null || employees.Count == 0)
            {
                return null;
            }
            else
            {
                return GetEmployeeDetails(employees);
            }
        }

        /// <summary>
        ///  Save Employee
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        public async Task<EmployeeDto?> SaveEmployee(SaveEmployeeDto employeeDto)
        {
            var employeeRequest = new SaveEmployeeRequest()
            {
                employeeId = 0,
                firstName = employeeDto.FirstName,
                lastName = employeeDto.LastName,
                dateOfBirth = employeeDto.DateOfBirth,
            };
            employeeDto.EmployeeId = await employeeRepository.SaveEmployeeAsync(employeeRequest);
            if (employeeDto.EmployeeId > 0)
            {
                if(employeeDto.Dependents?.Count() > 0)
                {
                    await SaveDependent(employeeDto);
                }
                await CalculateEnrolledBenefitCost(employeeDto);
            }
            return await GetEmployeeById(employeeDto.EmployeeId);
        }

        /// <summary>
        /// SaveDependent 
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        private async Task SaveDependent(SaveEmployeeDto employeeDto)
        {
            var dependent = GetDependentAsTable(employeeDto.Dependents, employeeDto.EmployeeId);
            var depedentRequest = new SaveDependentRequest()
            {
                dependent = dependent
            };
            //Calculate Benefits Cost
            await dependentRepository.SaveDependentAsync(depedentRequest);
        }

        /// <summary>
        ///  CalculateEnrolledBenefitCost
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        private async Task CalculateEnrolledBenefitCost(SaveEmployeeDto employeeDto)
        {
            //Calculate Benefits Cost
            var enrolledBenefitRequest = benefitCalculatorRuleEngine.CalculateEnrolledBenefits(employeeDto.Dependents, employeeDto.AnnualSalary);
            enrolledBenefitRequest.employeeId = employeeDto.EmployeeId;
            await SaveEnrolledBenefitAsync(enrolledBenefitRequest);
        }

        /// <summary>
        /// Save Enrolled Benefits
        /// </summary>
        /// <param name="enrolledBenefitRequest"></param>
        /// <returns></returns>
        private async Task SaveEnrolledBenefitAsync(EnrolledBenefitRequest enrolledBenefitRequest)
        {
            await employeeRepository.SaveEnrolledBenefitAsync(enrolledBenefitRequest);
        }

        /// <summary>
        /// GetDependentAsTable
        /// </summary>
        /// <param name="dependentDtoList"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable GetDependentAsTable(ICollection<DependentDto> dependentDtoList, int employeeId)
        {
            var dependentDt = new DataTable();

            dependentDt.Columns.Add("FirstName", typeof(string));
            dependentDt.Columns.Add("LastName", typeof(string));
            dependentDt.Columns.Add("EmployeeId", typeof(int));
            dependentDt.Columns.Add("BirthDate", typeof(DateTime));
            dependentDt.Columns.Add("Relation", typeof(int));

            foreach (var dependentDto in dependentDtoList)
            {
                dependentDt.Rows.Add(dependentDto.FirstName, dependentDto.LastName, employeeId, dependentDto.DateOfBirth, dependentDto.Relationship);
            }

            return dependentDt;
        }


        /// <summary>
        /// Validate Employee Rules
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public string InitiateValidationRules(SaveEmployeeDto employeeDto)
        {

            // To Check Employee has 1 spouse or domestic partner (not both)
            var isInValidSpouseEntry = employeeDto.Dependents
                                           .Where(s => s.Relationship == Relationship.Spouse || s.Relationship == Relationship.DomesticPartner)
                                           .Count() > 1;

            if (isInValidSpouseEntry)
            {
                return "An employee can only have 1 spouse or domestic partner (not both)";
            }
            else
            {
                return string.Empty;
            }

        }

    }
}
