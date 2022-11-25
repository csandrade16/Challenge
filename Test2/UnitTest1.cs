using EmployeeTurim.Domain.Entities;
using EmployeeTurim.Domain.Interfaces;
using EmployeeTurim.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Test2
{
    public class UnitTest1
    {
        [Fact(DisplayName = " ")]
        public async void AddMultipleEmployees_ShouldAddMultipleEmployees()
        {
            //Arrange
            Mock<IEmployeeRepository> repository = new Mock<IEmployeeRepository>();
            repository.Setup(r => r.GetEmployeeByRegistrationNumberAndName(It.IsAny<long>(), It.IsAny<string>())).Returns(Task.FromResult<Employee>(null));
            List<Employee> employees = new List<Employee>() { new Employee() { AdmissionDate = new DateTime(2021, 11, 16), Area = EmployeeTurim.Domain.Enums.Enum.EmployeeArea.BoardOfDirectors, Salary = 5000.00M, Name = "Kleyton Couto", RegistrationNumber = 1159, Position = "Diretor" } };

            //Act
            var result = await new EmployeeService(repository.Object).AddMultipleEmployees(employees);

            //Assert
            Assert.True(result.Count > 0);
        }


        [Fact(DisplayName = "")]
        public async void Remove_ShouldRemoveMultipleEmployees()
        {
            //Arrange
            Mock<IEmployeeRepository> repository = new Mock<IEmployeeRepository>();
            repository.Setup(r => r.GetEmployeeByRegistrationNumberAndName(It.IsAny<long>(), It.IsAny<string>())).Returns(Task.FromResult<Employee>(new Employee { AdmissionDate = new DateTime(2021, 11, 16), Area = EmployeeTurim.Domain.Enums.Enum.EmployeeArea.BoardOfDirectors, Salary = 5000.00M, Name = "Kleyton Couto", RegistrationNumber = 1159, Position = "Diretor" }));
            Dictionary<long, string> employees = new Dictionary<long, string>() { { 1159, "Kleyton Couto" } };

            //Act
            var result = await new EmployeeService(repository.Object).RemoveMultipleEmployees(employees);

            //Assert
            Assert.True(result.Count > 0);
        }

        [Fact(DisplayName = "")]
        public async void Remove_ShouldNotRemove()
        {
            //Arrange
            Mock<IEmployeeRepository> repository = new Mock<IEmployeeRepository>();
            repository.Setup(r => r.GetEmployeeByRegistrationNumberAndName(It.IsAny<long>(), It.IsAny<string>())).Returns(Task.FromResult<Employee>(null));
            Dictionary<long, string> employees = new Dictionary<long, string>() { { 1159, "Kleyton Couto" } };

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await new EmployeeService(repository.Object).RemoveMultipleEmployees(employees));
        }
    }
}