namespace Momenton.Repository.Entity
{
    /// <summary>
    /// Class Employee
    /// </summary>
    public class Employee
    {        
        public string EmployeeName { get; set; }

        public uint Id { get; set; }

        public uint? ManagerId { get; set; }        
    }
}
