using Api.Models;

namespace Api.DataAccesLayer.Model
{
    public class DependentDetailResponse
    {
        public int EmployeeId { get; set; }
        public int DependentId { get; set; }
        public string? D_FirstName { get; set; }
        public string? D_LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int? Relation { get; set; }
    }
}
