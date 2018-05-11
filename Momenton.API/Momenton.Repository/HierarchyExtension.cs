using Momenton.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Momenton.Repository
{    
    /// <summary>
    /// Static Class HierarchyExtension
    /// </summary>
    public static class HierarchyExtension
    {
        const int MAX_NO_OF_CEO = 1;

        /// <summary>
        /// Hierarchy extension method
        /// </summary>
        /// <param name="employees">The employees</param>        
        /// <param name="id">The id - null (for complete hierarchy) or Id of manager (for sub hierarchy)</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidHierarchyException"></exception>
        /// <returns>The hierarchy <see cref="EmployeeManager"/></returns>
        public static EmployeeManager Hierarchy(
                                                    this IList<Employee> employees,
                                                    uint? id = null
                                               )
        {
            if (employees == null)
            {
                throw new ArgumentNullException(nameof(employees));
            }

            //Get distinct employee list
            employees = employees.Distinct(new EmployeeEqualityComparer()).ToList();

            //Get sub hierarchy
            var hierarchy = employees.ComputeHierarchy(id);

            //If null then get complete company hierarchy
            if (!id.HasValue)
            {
                return hierarchy.FirstOrDefault();
            }
            else
            {
                //Get manager
                var manager = employees.Where(e => e.Id == id)
                                       .Select(e => new EmployeeManager { EmployeeName = e.EmployeeName, Id = e.Id, ManagerId = e.ManagerId })
                                       .SingleOrDefault();

                if (manager != null)
                {
                    //Add sub hierarchy
                    manager.Manages.AddRange(hierarchy);

                    return manager;
                }
                return null;
            }            
        }

        /// <summary>
        /// ComputeHierarchy extension method - Re-cursive function. n depth.
        /// </summary>
        /// <param name="employees">The employees</param>
        /// <param name="managerId">The manager id</param>        
        /// <param name="ancestors">The ancestors</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidHierarchyException"></exception>
        /// <returns>The managed employees <see cref="IList{EmployeeManager}"/></returns>
        private static IList<EmployeeManager> ComputeHierarchy(
                                                                    this IList<Employee> employees, 
                                                                    uint? managerId,
                                                                    List<EmployeeManager> ancestors = null
                                                              )
        {
            if (employees == null)
                throw new ArgumentNullException(nameof(employees));
                  
            //Get all employees managed by manager
            var manages = employees.Where(e => e != null && e.ManagerId == managerId && e.Id != managerId && !string.IsNullOrEmpty(e.EmployeeName))
                                   .OrderBy(e => e.Id)
                                   .Select(e => new EmployeeManager { EmployeeName = e.EmployeeName, Id = e.Id, ManagerId = e.ManagerId })
                                   .ToList();

            if (managerId == null && manages.Count > MAX_NO_OF_CEO)
            {
                throw new Exception("Company can have " + MAX_NO_OF_CEO + " CEO(s).");
            }

            //Check for invalid recursive hierarchy loop
            //Determine if a child is an ancestor of an ancestor
            if (ancestors != null && ancestors.Any()
                    && manages.Any(em => ancestors.IsAncestor(em.Id)))
            {
                throw new InvalidHierarchyException(manages, ancestors);                
            }            

            //If there exist employees managed by manager
            //re-cursively get all their managed employees
            if (manages.Any())
            {
                if (ancestors == null)
                    ancestors = new List<EmployeeManager>();

                //Build ancestors
                ancestors = ancestors.ToList();
                ancestors.AddRange(manages);

                manages.ForEach(e => e.Manages.AddRange(employees.ComputeHierarchy(e.Id, ancestors)));                
            }

            return manages;
        }

        /// <summary>
        /// IsAncestor extension method. Determine invalid hierarchy.
        /// Managee is manager - recursive loop in hierarchy
        /// </summary>
        /// <param name="ancestors">The ancestors</param>
        /// <param name="id">The managee id</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>True/False<see cref="bool"/></returns>
        public static bool IsAncestor(
                                            this IList<EmployeeManager> ancestors, 
                                            uint id
                                      )
        {
            if (ancestors == null)
                throw new ArgumentNullException(nameof(ancestors));

            return ancestors.Any(e => e.ManagerId == id);
        }        
    }
}
