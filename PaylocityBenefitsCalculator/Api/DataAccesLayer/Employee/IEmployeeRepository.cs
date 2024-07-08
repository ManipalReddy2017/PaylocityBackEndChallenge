using Api.DataAccesLayer.Model;
using Api.Models;

namespace Api.DataAccesLayer.Employee
{
    public interface IEmployeeRepository
    {
        public Task<List<EmployeeDetailResponse>> GetEmployeeById(EmployeeRequest employeeRequest);
        public Task<List<EmployeeDetailResponse>> GetEmployeesAll();
        public Task<int> SaveEmployeeAsync(SaveEmployeeRequest saveEmployee);
        public Task<bool> SaveEnrolledBenefitAsync(EnrolledBenefitRequest enrolledBenefitRequest);
    }
}
