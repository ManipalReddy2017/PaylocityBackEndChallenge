using Api.DataAccesLayer.Model;
using Api.Models;

namespace Api.DataAccesLayer.Dependent
{
    public interface IDependentRepository
    {
        public Task<int> SaveDependentAsync(SaveDependentRequest saveEmployee);
        public Task<List<DependentDetailResponse>> GetDependentById(DependentRequest dependentRequest);
        public Task<List<DependentDetailResponse>> GetDependentsAll();
    }
}
