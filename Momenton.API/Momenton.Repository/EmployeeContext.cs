using Momenton.Repository.Entity;
using System.Collections.Generic;

namespace Momenton.Repository
{
    /// <summary>
    /// Interface IEmployeeContext
    /// </summary>
    public interface IEmployeeContext
    {
        IList<Employee> Employees { get; }
    }

    /// <summary>
    /// Context - contains a list of employees
    /// </summary>
    public class EmployeeContext : IEmployeeContext
    {
        private IList<Employee> _employees =
                     new List<Employee> {

                            new Employee {  EmployeeName = "Alan", Id = 100, ManagerId = 150 }
                            , new Employee { EmployeeName = "Martin", Id = 220, ManagerId = 100 }
                            , new Employee { EmployeeName = "Jamie", Id = 150, ManagerId = null }
                            , new Employee { EmployeeName = "Alex", Id = 275, ManagerId = 100 }
                            , new Employee { EmployeeName = "Steve", Id = 400, ManagerId = 150 }
                            , new Employee { EmployeeName = "David", Id = 190, ManagerId = 400 }                                                                                                                                                         

                            //Bad data -- All eliminated from hierarchy
                            //, new Employee { EmployeeName = "Alex", Id = 275, ManagerId = 100 }
                            //, new Employee { EmployeeName = "Peter", Id = 170, ManagerId = null }
                            //, new Employee { EmployeeName = "", Id = 1000, ManagerId = 100 }
                            //, new Employee { EmployeeName = "Adam", Id = 1000, ManagerId = 4000 }
                            //, new Employee { EmployeeName = "Steve", Id = 400, ManagerId = 190 }
                            //, new Employee { EmployeeName = "Jamie", Id = 150, ManagerId = 190 }
                            //, new Employee { EmployeeName = "Jamie", Id = 150, ManagerId = 190 }
                        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employees">The employee list</param>
        public EmployeeContext (IList<Employee> employees = null)
        {
            if (employees != null)
            {
                _employees = employees;
            }            
        }

        /// <summary>
        /// Property Employees - Returns list of employees (dataset)
        /// </summary>
        public IList<Employee> Employees => _employees;
    }
}
