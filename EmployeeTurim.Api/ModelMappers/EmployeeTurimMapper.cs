using EmployeeTurim.Api.Models.RequestJSONs;
using EmployeeTurim.Api.ResquestJSONs;
using EmployeeTurim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static EmployeeTurim.Domain.Enums.Enum;

namespace EmployeeTurim.Api.ModelMappers
{
    public class EmployeeTurimMapper : IEmployeeTurimMapper
    {

        public List<Employee> MapAddEmployeesJSONListToEmployeeList(List<AddEmployeesJSON> employeeListJSON)
        {
            List<Employee> employees = new List<Employee>();

            foreach (AddEmployeesJSON employeeJSON in employeeListJSON)
            {
                Employee employee = new Employee();
                employee.RegistrationNumber = employeeJSON.RegistrationNumber;
                employee.Name = employeeJSON.Name;
                employee.Position = employeeJSON.Role;
                employee.AdmissionDate = employeeJSON.AdmissionDate.Date;
                employeeJSON.Salary = Regex.Replace(employeeJSON.Salary, @"[R$ .]", "");
                employee.Salary = Convert.ToDecimal(Regex.Replace(employeeJSON.Salary, @"[,]", "."));
                employeeJSON.Area = Regex.Replace(employeeJSON.Area, @"\s", "").ToUpper();

                switch (employeeJSON.Area)
                {
                    case "DIRETORIA":
                        employee.Area = EmployeeArea.Diretoria;
                        break;
                    case "CONTABILIDADE":
                        employee.Area = EmployeeArea.Contabilidade;
                        break;
                    case "FINANCEIRO":
                        employee.Area = EmployeeArea.Financeiro;
                        break;
                    case "TECNOLOGIA":
                        employee.Area = EmployeeArea.Tecnilogia;
                        break;
                    case "SERVIÇOSGERAIS":
                        employee.Area = EmployeeArea.ServicosGerais;
                        break;
                    case "RELACIONAMENTOCOMOCLIENTE":
                        employee.Area = EmployeeArea.RelacaoCliente;
                        break;
                    default:
                        throw new Exception();
                }
                employees.Add(employee);
            }
            return employees;
        }

        public Dictionary<long, string> MapRemoveEmployeesJSONToDictionary(List<RemoveEmployeesJSON> removeEmployeesJSONList)
        {
            Dictionary<long, string> registrationNumbersAndNames = new Dictionary<long, string>();

            foreach (RemoveEmployeesJSON removeEmployeeJSON in removeEmployeesJSONList)
            {
                registrationNumbersAndNames.Add(removeEmployeeJSON.RegistrationNumber, removeEmployeeJSON.Name);
            }
            return registrationNumbersAndNames;
        }
    }
}
