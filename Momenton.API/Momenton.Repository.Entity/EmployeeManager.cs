using System.Collections.Generic;

namespace Momenton.Repository.Entity
{
    /// <summary>
    /// Class EmployeeManager
    /// </summary>
    public class EmployeeManager : Employee
    {
        private List<EmployeeManager> _manages = new List<EmployeeManager>();

        public List<EmployeeManager> Manages
        {
            get
            {
                return _manages;
            }
            set
            {
                _manages = value;
            }
        }
    }
}
