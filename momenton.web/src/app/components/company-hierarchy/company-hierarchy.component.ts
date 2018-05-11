import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { EmployeeManager } from '../../models/company-hierarchy.models';

@Component({
  selector: 'app-company-hierarchy',
  templateUrl: './company-hierarchy.component.html',
  styleUrls: ['./company-hierarchy.component.scss']
})
export class CompanyHierarchyComponent implements OnInit {
  companyHierarchy: string;

  constructor(private http: HttpClient) { }

  async ngOnInit() {
    try {
      await this.http.get<EmployeeManager>("http://localhost:64800/api/company/hierarchy")
                     .subscribe(employeeManager => {
                         this.companyHierarchy = this.displayCompanyHierarchy(employeeManager, 0)
                     });    
    }
    catch(e) {
      alert(e);
    }    
  }

  displayCompanyHierarchy(manager: EmployeeManager, depth: number) : string {
    var tabs = Array(depth * 20).join("&nbsp;");
    var employeeName = tabs + manager.employeeName;

    if (manager.manages.length > 0)
    {
        manager.manages.forEach(e => employeeName = employeeName + "<br/>"  + this.displayCompanyHierarchy(e, depth + 1));
    }

    return employeeName;
  }

}
