using Momenton.Repository.Entity;

namespace Momenton.Repository
{
    /// <summary>
    /// Interface IEmployeeRepository
    /// </summary>
    public interface IEmployeeRepository
    {
        EmployeeManager GetCompanyHierarchy();
    }
}