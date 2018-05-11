using Momenton.Repository.Entity;
using System.Collections.Generic;

namespace Momenton.Repository
{
    /// <summary>
    /// Class EmployeeEqualityComparer
    /// </summary>
    public class EmployeeEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            if (!string.IsNullOrEmpty(x.EmployeeName) && !string.IsNullOrEmpty(y.EmployeeName))
            {
                return x.EmployeeName.Trim() == y.EmployeeName.Trim() && x.Id == y.Id && x.ManagerId == y.ManagerId;
            }
            return false;
        }

        public int GetHashCode(Employee obj)
        {
            return (string.IsNullOrEmpty(obj.EmployeeName) ? -1 : obj.EmployeeName.GetHashCode()) 
                ^ obj.Id.GetHashCode() ^ (obj.ManagerId.HasValue ? obj.ManagerId.Value.GetHashCode() : -1);
        }
    }
}
