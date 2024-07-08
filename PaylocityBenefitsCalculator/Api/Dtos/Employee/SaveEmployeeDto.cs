using Api.Dtos.Dependent;

namespace Api.Dtos.Employee;

public class SaveEmployeeDto
{
    public int EmployeeId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public decimal AnnualSalary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<DependentDto> Dependents { get; set; } = new List<DependentDto>();
}
