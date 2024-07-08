using Api.Dtos.Dependent;
using Api.Models;

namespace Api.Dtos.Employee;

public class EmployeeEnrolledBenefitDto
{
    public int EmployeeId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal AnnualSalary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<DependentDto>? Dependents { get; set; }
    public EnrolledBenefitDto EnrolledBenefit { get; set; }
}
