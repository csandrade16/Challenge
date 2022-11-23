using EmployeeTurim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTurim.Domain.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> AddMultipleEmployees(List<Employee> employees);
        Task<List<Employee>> RemoveMultipleEmployees(Dictionary<long, string> RegistrationNumbersAndNames);
        Task<List<Employee>> GetAllEmployees();
    }
}
