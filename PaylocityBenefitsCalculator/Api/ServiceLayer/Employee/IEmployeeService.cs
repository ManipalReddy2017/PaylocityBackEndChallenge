using Api.Dtos.Employee;

namespace Api.ServiceLayer.Employee
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetEmployeeById(int employeeId);
        Task<List<EmployeeDto>> GetEmployeeAll();
        Task<EmployeeDto?> SaveEmployee(SaveEmployeeDto employeeDto);
        string InitiateValidationRules(SaveEmployeeDto employeeDto);
    }
}
