using ProfitSharing.Domain.DTOs;

namespace ProfitSharing.Domain.Interfaces
{
    public interface IEmployeeTurimClient
    {
        Task<List<EmployeeDTO>> GetAllEmployees();
     }
}
