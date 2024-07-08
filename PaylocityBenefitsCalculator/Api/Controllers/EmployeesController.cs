using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.ServiceLayer.Employee;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService employeeService;
    public EmployeesController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<EmployeeDto>>> Get(int id)
    {
        try
        {
            var employeeDto = await employeeService.GetEmployeeById(id);
            if (employeeDto == null)
            {
                //Log the exception details
                return StatusCode(404, "NotFound");
            }
            else
            {
                return new ApiResponse<EmployeeDto>
                {
                    Data = employeeDto,
                    Success = true,
                    Status = System.Net.HttpStatusCode.OK
                };
            }
        }
        catch(Exception ex)
        {
            //Log the exception details
            return StatusCode(500, "Internal Server Error");
        }
    }

    [SwaggerOperation(Summary = "Add employee")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<EmployeeDto>>> AddEmployee(SaveEmployeeDto employeeDto)
    {
        try
        {
            var message = employeeService.InitiateValidationRules(employeeDto);
            if (string.IsNullOrEmpty(message))
            {
                var updatedEmployeeDto = await employeeService.SaveEmployee(employeeDto);
                return new ApiResponse<EmployeeDto>
                {
                    Data = updatedEmployeeDto,
                    Success = true,
                    Status= System.Net.HttpStatusCode.OK
                };
            }
            else
            {
                return new ApiResponse<EmployeeDto>
                {
                    Message = message,
                    Success = false,
                    Status = System.Net.HttpStatusCode.OK
                };
            }
        }
        catch(Exception ex)
        {
            //Implement the logging to Log the exception details to Kibana
            return StatusCode(500, "Internal Server Error");
        }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<EmployeeDto>>>> GetAll()
    {
        try
        {
            var employeeDtoList = await employeeService.GetEmployeeAll();
            if (employeeDtoList == null || employeeDtoList.Count == 0)
            {
                //Log the exception details
                return StatusCode(404, "NotFound");
            }
            else
            {
                return new ApiResponse<List<EmployeeDto>>
                {
                    Data = employeeDtoList,
                    Success = true,
                    Status = System.Net.HttpStatusCode.OK
                };
            }
        }
        catch (Exception ex)
        {
            //Log the exception details
            return StatusCode(500, "Internal Server Error");
        }

    }
}
