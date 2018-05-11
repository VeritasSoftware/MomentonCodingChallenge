export class Employee {        
    employeeName: string;
    id: number;
    manangerId?: number;        
}

export class EmployeeManager extends Employee
{
    manages: EmployeeManager[]        
}