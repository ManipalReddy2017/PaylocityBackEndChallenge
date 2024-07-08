using Api.Models;
using System.Data;
using System.Data.SqlClient;
using Api.DataAccesLayer.Model;
using Dapper;

namespace Api.DataAccesLayer.Dependent
{
    public class DependentRepository : IDependentRepository
    {
        private readonly string ConnectionString = null;

        private readonly IConfiguration configuration;
        public DependentRepository(IConfiguration config)
        {
            configuration = config;
            ConnectionString = configuration.GetConnectionString("Payroll");
        }

        public async Task<List<DependentDetailResponse>> GetDependentById(DependentRequest dependentRequest)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var reader = connection.QueryMultiple("sp_get_dependent_details", dependentRequest, commandType: CommandType.StoredProcedure);
                var employeeResponse = reader.Read<DependentDetailResponse>().ToList();
                return employeeResponse;
            }
        }

        public async Task<List<DependentDetailResponse>> GetDependentsAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var reader = connection.QueryMultiple("sp_get_dependent_details_all", null, commandType: CommandType.StoredProcedure);
                var employeeResponse = reader.Read<DependentDetailResponse>().ToList();
                return employeeResponse;
            }
        }

        public async Task<int> SaveDependentAsync(SaveDependentRequest saveDependent)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    var employeeId = await connection.ExecuteAsync("sp_save_dependent", saveDependent, commandType: CommandType.StoredProcedure);
                    return employeeId;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
