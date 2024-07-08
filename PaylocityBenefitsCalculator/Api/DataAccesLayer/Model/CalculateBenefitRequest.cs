using System.Data;

namespace Api.DataAccesLayer.Model
{
    public class CalculateBenefitRequest
    {
        public int employeeId { get; set; }
        public decimal baseSalary { get; set; }
        public DataTable dependent { get; set; }

    }
}
