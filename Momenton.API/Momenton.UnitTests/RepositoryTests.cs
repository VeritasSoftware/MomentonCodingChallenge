using Microsoft.VisualStudio.TestTools.UnitTesting;
using Momenton.Repository;
using Momenton.Repository.Entity;
using System.Collections.Generic;

namespace Momenton.UnitTests
{
    [TestClass]
    public class RepositoryTests
    {
        private IEmployeeContext GetEmployeeContext()
        {
            var employees = new List<Employee> {

                    new Employee {  EmployeeName = "Alan", Id = 100, ManagerId = 150 }
                    , new Employee { EmployeeName = "Martin", Id = 220, ManagerId = 100 }
                    , new Employee { EmployeeName = "Jamie", Id = 150, ManagerId = null }
                    , new Employee { EmployeeName = "Alex", Id = 275, ManagerId = 100 }
                    , new Employee { EmployeeName = "Steve", Id = 400, ManagerId = 150 }
                    , new Employee { EmployeeName = "David", Id = 190, ManagerId = 400 }                                                                                                                                                                             
                };

            return new EmployeeContext(employees);
        }

        [TestMethod]
        public void Test_GetCompanyHierarchy()
        {
            IEmployeeRepository employeeRepository = new EmployeeRepository(GetEmployeeContext());

            var hierarchy = employeeRepository.GetCompanyHierarchy();

            //CEO
            Assert.IsTrue(hierarchy.EmployeeName == "Jamie");

            Assert.IsTrue(hierarchy.Manages[0].EmployeeName == "Alan");
            Assert.IsTrue(hierarchy.Manages[0].Manages.Count == 2);
            Assert.IsTrue(hierarchy.Manages[0].Manages[0].EmployeeName == "Martin");
            Assert.IsTrue(hierarchy.Manages[0].Manages[1].EmployeeName == "Alex");            

            Assert.IsTrue(hierarchy.Manages[1].EmployeeName == "Steve");
            Assert.IsTrue(hierarchy.Manages[1].Manages.Count == 1);
            Assert.IsTrue(hierarchy.Manages[1].Manages[0].EmployeeName == "David");
        }

        [TestMethod]
        public void Test_Hierarchy_Extension()
        {
            List<Employee> employees = new List<Employee>();

            employees.AddRange(new Employee[] {
                new Employee {  EmployeeName = "Alan", Id = 100, ManagerId = 150 }
                , new Employee { EmployeeName = "Martin", Id = 220, ManagerId = 100 }
                , new Employee { EmployeeName = "Jamie", Id = 150, ManagerId = null }
                , new Employee { EmployeeName = "Alex", Id = 275, ManagerId = 100 }
                , new Employee { EmployeeName = "Steve", Id = 400, ManagerId = 150 }
                , new Employee { EmployeeName = "David", Id = 190, ManagerId = 400 }                                                
            });

            //Alan's (Id: 100) sub hierarchy
            var hierarchy = employees.Hierarchy(100);            

            Assert.IsTrue(hierarchy.EmployeeName == "Alan");
            Assert.IsTrue(hierarchy.Manages.Count == 2);
            Assert.IsTrue(hierarchy.Manages[0].EmployeeName == "Martin");
            Assert.IsTrue(hierarchy.Manages[1].EmployeeName == "Alex");

            //Complete company hierarchy
            hierarchy = employees.Hierarchy();

            //CEO
            Assert.IsTrue(hierarchy.EmployeeName == "Jamie");

            Assert.IsTrue(hierarchy.Manages[0].EmployeeName == "Alan");
            Assert.IsTrue(hierarchy.Manages[0].Manages.Count == 2);
            Assert.IsTrue(hierarchy.Manages[0].Manages[0].EmployeeName == "Martin");
            Assert.IsTrue(hierarchy.Manages[0].Manages[1].EmployeeName == "Alex");

            Assert.IsTrue(hierarchy.Manages[1].EmployeeName == "Steve");
            Assert.IsTrue(hierarchy.Manages[1].Manages.Count == 1);
            Assert.IsTrue(hierarchy.Manages[1].Manages[0].EmployeeName == "David");

            //Invalid Id: 1000
            hierarchy = employees.Hierarchy(1000);

            Assert.IsTrue(hierarchy == null);
        }

        [TestMethod]
        public void Test_Hierarchy_Extension_Invalid_Hierarchy_Loop()
        {
            List<Employee> employees = new List<Employee>();

            //Jamie is David's manager and David is also Jamie's manager. 
            //Across multiple depth levels.
            //Invalid recursive loop hierarchy
            employees.AddRange(new Employee[] {
                new Employee {  EmployeeName = "Alan", Id = 100, ManagerId = 150 }
                , new Employee { EmployeeName = "Martin", Id = 220, ManagerId = 100 }
                , new Employee { EmployeeName = "Jamie", Id = 150, ManagerId = null }
                , new Employee { EmployeeName = "Alex", Id = 275, ManagerId = 100 }
                , new Employee { EmployeeName = "Steve", Id = 400, ManagerId = 150 }
                , new Employee { EmployeeName = "David", Id = 190, ManagerId = 400 }     
                //Bad data making David as Jamie's manager           
                , new Employee { EmployeeName = "Jamie", Id = 150, ManagerId = 190 }
            });            

            try
            {
                //Complete company hierarchy
                var hierarchy = employees.Hierarchy();
            }
            catch(InvalidHierarchyException ex)
            {
                Assert.IsTrue(ex.Message == "Invalid hierarchy loop. Managee David(Id: 190) is a manager of manager Jamie(Id: 150).");
            }            
        }

        [TestMethod]
        public void Test_Hierarchy_Extension_Duplicate_Employee()
        {
            List<Employee> employees = new List<Employee>();

            //Duplicate entry for Alex
            employees.AddRange(new Employee[] {
                new Employee {  EmployeeName = "Alan", Id = 100, ManagerId = 150 }
                , new Employee { EmployeeName = "Martin", Id = 220, ManagerId = 100 }
                , new Employee { EmployeeName = "Jamie", Id = 150, ManagerId = null }
                , new Employee { EmployeeName = "Alex", Id = 275, ManagerId = 100 }
                , new Employee { EmployeeName = "Steve", Id = 400, ManagerId = 150 }
                , new Employee { EmployeeName = "David", Id = 190, ManagerId = 400 }
                , new Employee { EmployeeName = "Alex", Id = 275, ManagerId = 100 }
            });            

            //Complete company hierarchy
            var hierarchy = employees.Hierarchy();

            //CEO
            Assert.IsTrue(hierarchy.EmployeeName == "Jamie");

            Assert.IsTrue(hierarchy.Manages[0].EmployeeName == "Alan");
            Assert.IsTrue(hierarchy.Manages[0].Manages.Count == 2);
            Assert.IsTrue(hierarchy.Manages[0].Manages[0].EmployeeName == "Martin");
            //Only 1 Alex - duplicate removed
            Assert.IsTrue(hierarchy.Manages[0].Manages[1].EmployeeName == "Alex");

            Assert.IsTrue(hierarchy.Manages[1].EmployeeName == "Steve");
            Assert.IsTrue(hierarchy.Manages[1].Manages.Count == 1);
            Assert.IsTrue(hierarchy.Manages[1].Manages[0].EmployeeName == "David");            
        }
    }
}
