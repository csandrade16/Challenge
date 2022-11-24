using EmployeeTurim.Api.ModelMappers;
using EmployeeTurim.Api.Models.RequestJSONs;
using EmployeeTurim.Api.ResquestJSONs;
using EmployeeTurim.Domain.Entities;
using EmployeeTurim.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTurim.Api.Controllers
{
    [ApiController]
  
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class EmployeeTurimController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeTurimMapper _mapper;
        private readonly ILogger _logger;
        public EmployeeTurimController(IEmployeeService employeeService, IEmployeeTurimMapper employeeTurimMapper, ILogger<EmployeeTurimController> logger)
        {
            _employeeService = employeeService;
            _mapper = employeeTurimMapper;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("AddEmployees")]
        public async Task<IActionResult> AddEmployees(List<AddEmployeesJSON> employeesJSON)
        {
            try
            {
                TryValidateModel(employeesJSON);
                if (ModelState.IsValid)
                {
                    List<Employee> employees = _mapper.MapAddEmployeesJSONListToEmployeeList(employeesJSON);
                    employees = await _employeeService.AddMultipleEmployees(employees);
                    return Ok(employees);
                }
                else
                {
                    return BadRequest(employeesJSON);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("RemoveEmployees")]
        public async Task<IActionResult> RemoveEmployees(List<RemoveEmployeesJSON> removeEmployeesJSONs)
        {
            try
            {
                TryValidateModel(removeEmployeesJSONs);
                if (ModelState.IsValid)
                {
                    Dictionary<long, string> registrationNumbersAndNames = _mapper.MapRemoveEmployeesJSONToDictionary(removeEmployeesJSONs);
                    List<Employee> removedEmployees = await _employeeService.RemoveMultipleEmployees(registrationNumbersAndNames);
                    return Ok(removedEmployees);
                }
                else
                {
                    return BadRequest(removeEmployeesJSONs);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                List<Employee> AllEmployees = await _employeeService.GetAllEmployees();
                return Ok(AllEmployees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
