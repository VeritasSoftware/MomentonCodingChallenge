using Momenton.Repository.Entity;
using System.Collections.Generic;

namespace Momenton.Repository
{
    /// <summary>
    /// Class EmployeeRepository
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> Employees { get; set; }

        public EmployeeRepository()
        {
            this.Employees = new List<Employee>();

            this.Employees.AddRange(new Employee[] {

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
            });
        }

        /// <summary>
        /// Interface method: GetCompanyHierarchy
        /// </summary>
        /// <returns>The company hierarchy <see cref="EmployeeManager"/></returns>
        public EmployeeManager GetCompanyHierarchy()
        {
            //Get complete company hierarchy
            return this.Employees.Hierarchy();
        }
    }
}