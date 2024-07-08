using Api.Models;

namespace Api.DataAccesLayer.Model
{
    public class EmployeeDetailResponse
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal TotalBaseSalary { get; set; }
        public int? DependentId { get; set; }
        public string? D_FirstName { get; set; }
        public string? D_LastName { get; set; }
        public DateTime E_DateOfBirth { get; set; }
        public DateTime D_DateOfBirth { get; set; }
        public int? Relation { get; set; }

    }
}
