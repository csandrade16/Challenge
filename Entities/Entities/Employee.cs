using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Enum.Enum;

namespace Entities.Entities
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public long RegistrationNumber { get; set; }
        public string Name { get; set; }
        public EmployeeArea Area { get; set; }
        public string Role { get; set; }
        public decimal GrossSalary { get; set; }
        public DateTime AdmissionDate { get; set; }

    }
}
}
