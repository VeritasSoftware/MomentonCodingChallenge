using System.Collections.Generic;

namespace Momenton.Repository.Entity
{
    /// <summary>
    /// Class EmployeeManager - Recursive
    /// </summary>
    public class EmployeeManager : Employee
    {
        private List<EmployeeManager> _manages = new List<EmployeeManager>();

        public List<EmployeeManager> Manages => _manages;        
    }
}
