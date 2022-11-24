using EmployeeTurim.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTurim.Repository
{
     public class EmployeeRepositorySettings : IEmployeeRepositorySettings
    {       
        public string EmployeeCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
      
    }
}
