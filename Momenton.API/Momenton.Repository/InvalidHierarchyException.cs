using Momenton.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Momenton.Repository
{
    /// <summary>
    /// Class InvalidHierarchyException
    /// </summary>
    public class InvalidHierarchyException : Exception
    {
        private static string GetMessage(
                                            IList<EmployeeManager> manages,
                                            IList<EmployeeManager> ancestors
                                        )
        {
            if (manages == null)
                throw new ArgumentNullException(nameof(manages));

            if (ancestors == null)
                throw new ArgumentNullException(nameof(ancestors));

            string message = "Invalid hierarchy loop.";

            manages.Where(em => ancestors.IsAncestor(em.Id)).ToList().ForEach(em =>
            {
                var ancestor = ancestors.First(em1 => em1.Id == em.ManagerId);
                var childDetails = ancestor.EmployeeName + "(Id: " + ancestor.Id + ")";
                var ancestorDetails = em.EmployeeName + "(Id: " + em.Id + ")";

                message = message + $" Managee {childDetails} is a manager of manager {ancestorDetails}.";
            });

            return message;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manages">The managees</param>
        /// <param name="ancestors">The ancestors</param>
        public InvalidHierarchyException(
                                            IList<EmployeeManager> manages, 
                                            IList<EmployeeManager> ancestors
                                        )        
                                        : base (GetMessage(manages, ancestors))
        {
            
        }
    }
}
