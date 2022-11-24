using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using static EmployeeTurim.Domain.Enums.Enum;

namespace EmployeeTurim.Domain.Entities
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public long RegistrationNumber { get; set; }
        public string Name { get; set; }    
        public EmployeeArea Area { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime AdmissionDate { get; set; }

    }
}
