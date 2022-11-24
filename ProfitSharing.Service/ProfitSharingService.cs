using Microsoft.VisualBasic;
using ProfitSharing.Domain.DTOs;
using ProfitSharing.Domain.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;
using static ProfitSharing.Domain.Enums.Enum;

namespace ProfitSharing.Service
{
    public class ProfitSharingService : IProfitSharingService
    {
        private readonly IEmployeeTurimClient _employeeTurimClient;

        public ProfitSharingService(IEmployeeTurimClient employeeTurimClient)
        {
            _employeeTurimClient = employeeTurimClient;
        }

        public async Task<ProfitSharingResultDTO> CalculateProfitSharing(decimal availableSum)
        {
            List<EmployeeDTO> employees = await _employeeTurimClient.GetAllEmployees();
            if(employees == null)
                throw new Exception();

            List<ProfitSharingProfileDTO> profitSharingProfileList = CreateProfitSharinfProfile(employees);
            ProfitSharingResultDTO profitSharingResultDTO = CalculateTotalToShare(profitSharingProfileList, availableSum);

            return profitSharingResultDTO;
        }

        private List<ProfitSharingProfileDTO> CreateProfitSharinfProfile(List<EmployeeDTO> employees)
        {
            List<ProfitSharingProfileDTO> profitSharingProfileList = new List<ProfitSharingProfileDTO>();

            foreach (EmployeeDTO employee in employees)
            {
                ProfitSharingProfileDTO profitSharingProfile = new ProfitSharingProfileDTO();

                profitSharingProfile.Name = employee.Name;
                employee.Role = Regex.Replace(employee.Role, @"\s", "").ToUpper();
                if (employee.Role == "ESTAGIARIO" || employee.Role == "ESTAGIÁRIO")
                {
                    profitSharingProfile.IsIntern = true;
                }
                profitSharingProfile.RegistrationNumber = Convert.ToString(employee.RegistrationNumber).PadLeft(7, '0');
                CalculateIndividualAreaWeight(profitSharingProfile, employee);
                CalculateIndividualSalaryWeight(profitSharingProfile, employee);
                CalculateIndividualTimeWeight(profitSharingProfile, employee);
                CalculateIndividualProfitSharingSum(profitSharingProfile, employee);

                profitSharingProfileList.Add(profitSharingProfile);
            }
            return profitSharingProfileList;
        }

        private void CalculateIndividualAreaWeight(ProfitSharingProfileDTO profitSharingProfile, EmployeeDTO employee)
        {
            switch (employee.Area)
            {
                case EmployeeArea.Diretoria:
                    profitSharingProfile.AreaWeight = 1;
                    break;
                case EmployeeArea.Contabilidade:
                    profitSharingProfile.AreaWeight = 2;
                    break;
                ///case EmployeeArea.Financeiro:
                ///    profitSharingProfile.AreaWeight = 2;
                ///    break;
                ///case EmployeeArea.Tecnologia:
                ///    profitSharingProfile.AreaWeight = 2;
                ///    break;
                case EmployeeArea.ServicosGerais:
                    profitSharingProfile.AreaWeight = 3;
                    break;
                case EmployeeArea.RelacaoCliente:
                    profitSharingProfile.AreaWeight = 5;
                    break;
                default:
                    throw new Exception("Erro ao mapear a área informada");
            }
        }

        private void CalculateIndividualSalaryWeight(ProfitSharingProfileDTO profitSharingProfile, EmployeeDTO employee)
        {
            decimal minimumWage = 1000.00M;

            if (profitSharingProfile.IsIntern == true || (employee.Salary <= (minimumWage * 3)))
                profitSharingProfile.SalaryWeight = 1;

            else if (employee.Salary > (minimumWage * 3) && employee.Salary <= (minimumWage * 5))
                profitSharingProfile.SalaryWeight = 2;

            else if (employee.Salary > (minimumWage * 5) && employee.Salary <= (minimumWage * 8))
                profitSharingProfile.SalaryWeight = 3;

            else if (employee.Salary > (minimumWage * 8))
                profitSharingProfile.SalaryWeight = 5;
        }

        private void CalculateIndividualTimeWeight(ProfitSharingProfileDTO profitSharingProfile, EmployeeDTO employee)
        {
            DateTime today = DateTime.Now.Date;
            long yearsUntilNow = Math.Abs(DateAndTime.DateDiff(DateInterval.Year, today, employee.AdmissionDate));

            if (yearsUntilNow <= 1)
                profitSharingProfile.TimeWeight = 1;

            else if (yearsUntilNow > 1 && yearsUntilNow <= 3)
                profitSharingProfile.TimeWeight = 2;

            else if (yearsUntilNow > 3 && yearsUntilNow <= 8)
                profitSharingProfile.TimeWeight = 3;

            else if (yearsUntilNow > 8)
                profitSharingProfile.TimeWeight = 4;

        }

        private void CalculateIndividualProfitSharingSum(ProfitSharingProfileDTO profitSharingProfile, EmployeeDTO employee)
        {
            int months = 12;
            profitSharingProfile.IndividualProfitSharingSum = (((employee.Salary * profitSharingProfile.TimeWeight) + (employee.Salary * profitSharingProfile.AreaWeight)) / profitSharingProfile.SalaryWeight) * months;
        }

        private ProfitSharingResultDTO CalculateTotalToShare(List<ProfitSharingProfileDTO> profitSharingProfileList, decimal availableSum)
        {

            decimal idealTotalAmount = profitSharingProfileList.Sum(psp => psp.IndividualProfitSharingSum);
            decimal percentualDifference = ((((availableSum * 100) / idealTotalAmount) - 100)) / -100;

            ProfitSharingResultDTO profitSharingResult = new ProfitSharingResultDTO();
            if (idealTotalAmount <= availableSum)
            {
                foreach (ProfitSharingProfileDTO profitSharingProfile in profitSharingProfileList)
                {
                    ProfitSharingParticipant profitSharingParticipant = new ProfitSharingParticipant();
                    profitSharingParticipant.ResultingIndividualProfitSharingSum = profitSharingProfile.IndividualProfitSharingSum.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
                    profitSharingParticipant.Name = profitSharingProfile.Name;
                    profitSharingParticipant.RegistrationNumber = profitSharingProfile.RegistrationNumber;
                    profitSharingResult.ProfitSharingParticipantList.Add(profitSharingParticipant);
                }
                profitSharingResult.SharedSum = idealTotalAmount.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
                profitSharingResult.BalanceSum = (availableSum - idealTotalAmount).ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
            }
            else
            {
                List<decimal> resultingSumList = new List<decimal>();
                foreach (ProfitSharingProfileDTO profitSharingProfile in profitSharingProfileList)
                {
                    ProfitSharingParticipant profitSharingParticipant = new ProfitSharingParticipant();
                    Decimal individualResultingSum = profitSharingProfile.IndividualProfitSharingSum - (profitSharingProfile.IndividualProfitSharingSum * percentualDifference);
                    profitSharingParticipant.ResultingIndividualProfitSharingSum = individualResultingSum.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
                    profitSharingParticipant.Name = profitSharingProfile.Name;
                    profitSharingParticipant.RegistrationNumber = profitSharingProfile.RegistrationNumber;
                    profitSharingResult.ProfitSharingParticipantList.Add(profitSharingParticipant);
                    resultingSumList.Add(individualResultingSum);
                }
                decimal totalResultingSum = resultingSumList.Sum(sum => sum);

                profitSharingResult.SharedSum = totalResultingSum.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
                profitSharingResult.BalanceSum = (availableSum - totalResultingSum).ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
            }
            profitSharingResult.TotalAvailableSum = availableSum.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
            profitSharingResult.TotalParticipants = profitSharingResult.ProfitSharingParticipantList.Count();

            return profitSharingResult;
        }
    }
}
