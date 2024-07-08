using Api.DataAccesLayer.Dependent;
using Api.DataAccesLayer.Model;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using System.Data;

namespace Api.ServiceLayer.Dependent
{
    /// <summary>
    /// DependentService
    /// </summary>
    public class DependentService : IDependentService
    {
        public IDependentRepository dependentRepository;
        public DependentService(IDependentRepository depedentRepository)
        {
            dependentRepository = depedentRepository;
        }

        /// <summary>
        ///  Get Dependents By Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<DependentDto?> GetDependentById(int dependentId)
        {
            var depenentRequest = new DependentRequest()
            {
                DependentId = dependentId
            };
            var dependents = await dependentRepository.GetDependentById(depenentRequest);
            if (dependents == null || dependents.Count == 0)
            {
                return null;
            }
            else
            {
                return GetDepedentDetails(dependents)?.FirstOrDefault();
            }
        }

        /// <summary>
        /// GetDependentAll
        /// </summary>
        /// <returns></returns>
        public async Task<List<DependentDto>?> GetDependentAll()
        {
            var dependents = await dependentRepository.GetDependentsAll();
            if (dependents == null || dependents.Count == 0)
            {
                return null;
            }
            else
            {
                return GetDepedentDetails(dependents);
            }
        }

        private List<DependentDto> GetDepedentDetails(List<DependentDetailResponse> dependentDetailResponses)
        {
            return dependentDetailResponses.Select(s => new DependentDto()
            {
                DateOfBirth = s.BirthDate,
                FirstName = s.D_FirstName,
                LastName = s.D_LastName,
                DependentId = s.DependentId,
                Relationship = s.Relation != null ? (Relationship)s.Relation : Relationship.None,
            }).ToList();
        }


    }
}
