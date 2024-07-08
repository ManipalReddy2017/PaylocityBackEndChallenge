using Api.Models;
using System.Data;
using System.Data.SqlClient;
using Api.DataAccesLayer.Model;
using Dapper;

namespace Api.DataAccesLayer.Employee
{
    public class EnrolledBenefitsRepository : IEnrolledBenefitsRepository
    {
        private readonly string ConnectionString = null;

        private readonly IConfiguration configuration;
        public EnrolledBenefitsRepository(IConfiguration config)
        {
            configuration = config;
            ConnectionString = configuration.GetConnectionString("Payroll");
        }

        public async Task<List<EnrolledBenefitsDetailResponse>> GetEnrolledBenefitsId(EmployeeRequest employeeRequest)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var reader = connection.QueryMultiple("sp_get_enrolled_benefits", employeeRequest, commandType: CommandType.StoredProcedure);
                var employeeResponse = reader.Read<EnrolledBenefitsDetailResponse>().ToList();
                return employeeResponse;
            }
        }

        public async Task<List<EnrolledBenefitsDetailResponse>> GetEnrolledBenefitsAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var reader = connection.QueryMultiple("sp_get_enrolled_benefits_all", null, commandType: CommandType.StoredProcedure);
                var employeeResponse = reader.Read<EnrolledBenefitsDetailResponse>().ToList();
                return employeeResponse;
            }
        }

    }
}
