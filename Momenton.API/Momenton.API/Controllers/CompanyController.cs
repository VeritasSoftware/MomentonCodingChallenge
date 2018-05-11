using Microsoft.AspNetCore.Mvc;
using Momenton.Repository;
using Momenton.Repository.Entity;
using System;

namespace Momenton.API.Controllers
{
    /// <summary>
    /// Company controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Company")]
    public class CompanyController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository">The injected employee repository</param>
        public CompanyController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        /// <summary>
        /// Get company heirarchy
        /// </summary>
        /// <returns><see cref="EmployeeManager"/></returns>
        [HttpGet("hierarchy")]
        public EmployeeManager GetCompanyHierarchy()
        {
            return _employeeRepository.GetCompanyHierarchy();
        }
    }
}