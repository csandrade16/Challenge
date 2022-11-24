using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeTurim.Api.Models.RequestJSONs
{
    public class RemoveEmployeesJSON
    {
        [Range(1, int.MaxValue)]
        [JsonPropertyName("matricula")]
        public long RegistrationNumber { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }
    }
}
