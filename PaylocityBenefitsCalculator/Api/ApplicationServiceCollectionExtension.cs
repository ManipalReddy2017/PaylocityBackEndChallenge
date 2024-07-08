using Api.DataAccesLayer.Dependent;
using Api.DataAccesLayer.Employee;
using Api.ServiceLayer.BenefitRuleEngine;
using Api.ServiceLayer.Dependent;
using Api.ServiceLayer.Employee;

namespace Api
{
    public static class ApplicationServiceCollectionExtension
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDependentService, DependentService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDependentRepository, DependentRepository>();
            services.AddScoped<IEnrolledBenefitsService, EnrolledBenefitsService>();
            services.AddScoped<IEnrolledBenefitsRepository, EnrolledBenefitsRepository>();
            services.AddScoped<IBenefitsCalculatorRuleEngine, BenefitsCalculatorRuleEngine>();
        }
    }
}
