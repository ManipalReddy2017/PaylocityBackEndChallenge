using Api.Models;
using System.Data;

namespace Api.DataAccesLayer.Model
{
    public class SaveDependentRequest
    {
        public DataTable? dependent  { get; set; }
    }
}
