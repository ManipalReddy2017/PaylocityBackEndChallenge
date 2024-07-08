using Api.DataAccesLayer.Model;
using Api.Dtos.Employee;

namespace Api.ServiceLayer.Employee
{
    public interface IEnrolledBenefitsService
    {
        public Task<EmployeeEnrolledBenefitDto?> GetEnrolledBenefitsId(int employeeId);
        public Task<List<EmployeeEnrolledBenefitDto>?> GetEnrolledBenefits();
    }
}
