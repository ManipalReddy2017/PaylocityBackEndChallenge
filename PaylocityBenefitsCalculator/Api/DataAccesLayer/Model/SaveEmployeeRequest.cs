using System.Data;

namespace Api.DataAccesLayer.Model
{
    public class SaveEmployeeRequest
    {
        public int? employeeId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public DateTime dateOfBirth { get; set; }

    }
}
