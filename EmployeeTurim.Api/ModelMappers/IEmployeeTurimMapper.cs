using EmployeeTurim.Api.Models.RequestJSONs;
using EmployeeTurim.Api.ResquestJSONs;
using EmployeeTurim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTurim.Api.ModelMappers
{
    public interface IEmployeeTurimMapper
    {
        List<Employee> MapAddEmployeesJSONListToEmployeeList(List<AddEmployeesJSON> employeeListJSON);

        Dictionary<long, string> MapRemoveEmployeesJSONToDictionary(List<RemoveEmployeesJSON> removeEmployeesJSONList);
    }
}
