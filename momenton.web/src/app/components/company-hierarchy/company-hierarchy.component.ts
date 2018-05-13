import { Component, OnInit } from '@angular/core';

import { EmployeeManager } from '../../models/company-hierarchy.models';
import { CompanyService } from '../../services/company-service/company-service';

@Component({
  selector: 'app-company-hierarchy',
  templateUrl: './company-hierarchy.component.html',
  styleUrls: ['./company-hierarchy.component.scss'],
  providers: [ CompanyService ]
})
export class CompanyHierarchyComponent implements OnInit {
  companyHierarchy: string;

  /**************************************/
  /* Constructor                        */
  /* http: The injected Company Service */
  /**************************************/  
  constructor(private companyService: CompanyService) { }

  async ngOnInit() {
    try {
      this.companyHierarchy = "";

      //Make async http call to get company hierarchy using the CompanyService
      await this.companyService.GetCompanyHierarchy(heirarchy => this.companyHierarchy = heirarchy);
    }
    catch(e) {
      alert(e);
    }    
  }  

}
