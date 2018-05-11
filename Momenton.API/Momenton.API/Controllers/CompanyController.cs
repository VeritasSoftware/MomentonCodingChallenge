using Microsoft.AspNetCore.Mvc;
using Momenton.Repository;
using Momenton.Repository.Entity;
using System;

namespace Momenton.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Company")]
    public class CompanyController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CompanyController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [HttpGet("hierarchy")]
        public EmployeeManager GetCompanyHierarchy()
        {
            return _employeeRepository.GetCompanyHierarchy();
        }
    }
}