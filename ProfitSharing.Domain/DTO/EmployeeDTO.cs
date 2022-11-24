using System.Text.Json.Serialization;
using static ProfitSharing.Domain.Enums.Enum;

namespace ProfitSharing.Domain.DTOs
{
    public class EmployeeDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("registrationNumber")]
        public long RegistrationNumber { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("area")]
        public EmployeeArea Area { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("salary")]
        public decimal Salary { get; set; }
        [JsonPropertyName("admissionDate")]
        public DateTime AdmissionDate { get; set; }
    }
}
