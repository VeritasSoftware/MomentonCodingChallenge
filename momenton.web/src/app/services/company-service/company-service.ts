import {Injectable} from '@angular/core'
import { HttpClient } from '@angular/common/http';

import { ICompanyService } from './icompany-service';
import { EmployeeManager } from '../../models/company-hierarchy.models';

@Injectable()
export class CompanyService implements ICompanyService {
    private apiBaseUrl:string = "http://localhost:64800/api/Company/";

    /*********************************/
    /* Constructor *******************/
    /* http: The injected HttpClient */
    /*********************************/
    constructor(private http: HttpClient){

    }

    /*************************************/
    /* Get the company hierarchy         */
    /* Make asycn http call to API       */
    /* Pass hierarchy string to delegate */
    /*************************************/
    async GetCompanyHierarchy(hierarchy: (string)=> void) {
        var url = this.apiBaseUrl + "hierarchy";
        await this.http.get<EmployeeManager>(url)
                       .subscribe(employeeManager => {
                            hierarchy(this.displayCompanyHierarchy(employeeManager, 0))
                       });  
    }

  /*******************************************************/
  /* Recursive function to display the company hierarchy */
  /*******************************************************/
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
