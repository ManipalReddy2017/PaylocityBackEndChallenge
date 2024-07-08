using Api.DataAccesLayer.Model;
using Api.Models;

namespace Api.DataAccesLayer.Employee
{
    public interface IEnrolledBenefitsRepository
    {
        public Task<List<EnrolledBenefitsDetailResponse>> GetEnrolledBenefitsId(EmployeeRequest employeeRequest);
        public Task<List<EnrolledBenefitsDetailResponse>> GetEnrolledBenefitsAll();
    }
}
