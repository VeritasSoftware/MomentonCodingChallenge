using Momenton.Repository.Entity;
using System;

namespace Momenton.Repository
{
    /// <summary>
    /// Class EmployeeRepository
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private IEmployeeContext _employeeContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeContext">The injected employee context</param>
        public EmployeeRepository(IEmployeeContext employeeContext)
        {
            _employeeContext = employeeContext ?? throw new ArgumentNullException(nameof(employeeContext));
        }        

        /// <summary>
        /// Interface method: GetCompanyHierarchy
        /// </summary>
        /// <returns>The company hierarchy <see cref="EmployeeManager"/></returns>
        public EmployeeManager GetCompanyHierarchy()
        {
            //Get complete company hierarchy
            return _employeeContext.Employees?.Hierarchy();
        }
    }
}