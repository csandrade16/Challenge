using EmployeeTurim.Domain.Interfaces;

namespace EmployeeTurim.Repository.Repository
{
    public class EmployeeRepositorySettings : IEmployeeRepositorySettings
    {
        public string EmployeeCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
