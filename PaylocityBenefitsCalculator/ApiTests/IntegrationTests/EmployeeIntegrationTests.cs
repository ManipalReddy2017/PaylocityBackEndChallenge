using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

public class EmployeeIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees");
        var employees = new List<EmployeeDto>
        {
            new()
            {
                EmployeeId = 1,
                FirstName = "LeBron",
                LastName = "James",
                AnnualSalary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                EmployeeId = 2,
                FirstName = "Ja",
                LastName = "Morant",
                AnnualSalary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<DependentDto>
                {
                    new()
                    {
                        DependentId = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        DependentId = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        DependentId = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                EmployeeId = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                AnnualSalary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<DependentDto>
                {
                    new()
                    {
                        DependentId = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/1");
        var employee = new EmployeeDto
        {
            EmployeeId = 1,
            FirstName = "LeBron",
            LastName = "James",
            AnnualSalary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30),
            Dependents = new List<DependentDto>()
        };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }
    
    [Fact]
    //task: make test pass
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

