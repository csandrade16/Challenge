using Microsoft.Extensions.Logging;
using ProfitSharing.Domain.DTOs;
using ProfitSharing.Domain.Interfaces;
using System.Text.Json;

namespace ProfitSharing.Infrastructure.Integrations.Clients
{
    public class EmployeeTurimClient : IEmployeeTurimClient
    {
        private HttpClient _client;
        private readonly ILogger<EmployeeTurimClient> _logger;
        private string URI;
        private string AccessKey;
        public EmployeeTurimClient(IEmployeeTurimClientSettings settings, HttpClient client, ILogger<EmployeeTurimClient>logger)
        {
            URI = settings.URI;
            AccessKey = settings.AccessKey;
            _client = client;
            _logger = logger;
        }
        public async Task <List<EmployeeDTO>> GetAllEmployees()//todo: tentar colocar o timout
        {
            try
            {
                //using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(25));
                using var responseStream = await _client.GetStreamAsync(URI);
                List<EmployeeDTO> GetAllEmployees = await JsonSerializer.DeserializeAsync<List<EmployeeDTO>>(responseStream);
                
                return  GetAllEmployees;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Não foi possivel conectar com a EmployeeTurimAPI");
                return null;
            }
        }
             
    }
}
