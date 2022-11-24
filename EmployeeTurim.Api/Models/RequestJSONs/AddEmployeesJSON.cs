using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


namespace EmployeeTurim.Api.ResquestJSONs
{
    public class AddEmployeesJSON
    {
        [Range(1, int.MaxValue)]
        [JsonPropertyName("matricula")]
        public long RegistrationNumber { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("area")]
        public string Area { get; set; }

        [JsonPropertyName("cargo")]
        public string Role { get; set; }

        [JsonPropertyName("salario_bruto")]
        public string GrossSalary { get; set; }

        [JsonPropertyName("data_de_admissao")]
        public DateTime AdmissionDate { get; set; }
    }
}
