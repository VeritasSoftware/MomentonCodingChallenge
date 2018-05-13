export class Employee {        
    employeeName: string;
    id: number;
    managerId?: number;        
}

export class EmployeeManager extends Employee
{
    manages: EmployeeManager[]        
}