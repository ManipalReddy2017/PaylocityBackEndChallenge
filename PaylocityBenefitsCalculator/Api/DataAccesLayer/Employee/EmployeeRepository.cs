using Api.Models;
using System.Data;
using System.Data.SqlClient;
using Api.DataAccesLayer.Model;
using Dapper;

namespace Api.DataAccesLayer.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string ConnectionString = null;

        private readonly IConfiguration configuration;
        public EmployeeRepository(IConfiguration config)
        {
            configuration = config;
            ConnectionString = configuration.GetConnectionString("Payroll");
        }

        public async Task<List<EmployeeDetailResponse>> GetEmployeeById(EmployeeRequest employeeRequest)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var reader = connection.QueryMultiple("sp_get_employee_details", employeeRequest, commandType: CommandType.StoredProcedure);
                var employeeResponse = reader.Read<EmployeeDetailResponse>().ToList();
                return employeeResponse;
            }
        }

        public async Task<List<EmployeeDetailResponse>> GetEmployeesAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var reader = connection.QueryMultiple("sp_get_employee_details_all", null, commandType: CommandType.StoredProcedure);
                var employeeResponse = reader.Read<EmployeeDetailResponse>().ToList();
                return employeeResponse;
            }
        }

        public async Task<int> SaveEmployeeAsync(SaveEmployeeRequest saveEmployee)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    var employeeId = await connection.ExecuteScalarAsync<int>("sp_save_employee", saveEmployee, commandType: CommandType.StoredProcedure);
                    return employeeId;
                }
            }
            catch (Exception ex)
            {
                // Log the exception details to Kibana using Serilog
                return 0;
            }
        }

        public async Task<bool> SaveEnrolledBenefitAsync(EnrolledBenefitRequest enrolledBenefitRequest)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    await connection.ExecuteAsync("sp_Save_EnrolledBenefits", enrolledBenefitRequest, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception details to Kibana using Serilog
                return false;
            }
        }

    }
}
