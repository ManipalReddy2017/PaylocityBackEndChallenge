using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.ServiceLayer.Employee;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EnrolledBenefitsController : ControllerBase
{
    private readonly IEnrolledBenefitsService enrolledBenefitsService;
    public EnrolledBenefitsController(IEnrolledBenefitsService enrolledBenefitsService)
    {
        this.enrolledBenefitsService = enrolledBenefitsService;
    }

    [SwaggerOperation(Summary = "Get Enrolled Benefits by EmployeeId")]
    [HttpGet("{employeeId}")]
    public async Task<ActionResult<ApiResponse<EmployeeEnrolledBenefitDto>>> GetEnrolledBenefits(int employeeId)
    {
        try
        {
            var employeeEnrolledBenefitDto = await enrolledBenefitsService.GetEnrolledBenefitsId(employeeId);
            if (employeeEnrolledBenefitDto == null)
            {
                //Log the exception details
                return StatusCode(404, "NotFound");
            }
            else
            {
                return new ApiResponse<EmployeeEnrolledBenefitDto>
                {
                    Data = employeeEnrolledBenefitDto,
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


    [SwaggerOperation(Summary = "Get all Employees Enrolled Benefits")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<EmployeeEnrolledBenefitDto>>>> GetEnrolledBenefits()
    {
        try
        {
            var employeeEnrolledBenefitDto = await enrolledBenefitsService.GetEnrolledBenefits();
            if (employeeEnrolledBenefitDto == null)
            {
                //Log the exception details
                return StatusCode(404, "NotFound");
            }
            else
            {
                return new ApiResponse<List<EmployeeEnrolledBenefitDto>>
                {
                    Data = employeeEnrolledBenefitDto,
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
