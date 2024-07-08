using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.ServiceLayer.Dependent;
using Api.ServiceLayer.Employee;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentService dependentService;
    public DependentsController(IDependentService dependentService)
    {
        this.dependentService = dependentService;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{dependentId}")]
    public async Task<ActionResult<ApiResponse<DependentDto>>> Get(int dependentId)
    {
        try
        {
            var depedentDetails = await this.dependentService.GetDependentById(dependentId);
            if (depedentDetails == null)
            {
                //Log the exception details
                return StatusCode(404, "NotFound");
            }
            else
            {
                return new ApiResponse<DependentDto>
                {
                    Data = depedentDetails,
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

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<DependentDto>>>> GetAll()
    {
        try
        {
            var dependentDetails = await this.dependentService.GetDependentAll();
            if (dependentDetails == null || dependentDetails.Count == 0)
            {
                //Log the exception details
                return StatusCode(404, "NotFound");
            }
            else
            {
                return new ApiResponse<List<DependentDto>>
                {
                    Data = dependentDetails,
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
