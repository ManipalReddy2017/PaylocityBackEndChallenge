using Api.Dtos.Dependent;
using Api.Dtos.Employee;

namespace Api.ServiceLayer.Dependent
{
    public interface IDependentService
    {
        Task<DependentDto> GetDependentById(int dependentId);
        Task<List<DependentDto>> GetDependentAll();
    }
}
