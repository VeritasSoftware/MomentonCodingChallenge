import { Routes } from '@angular/router';

import { AppComponent } from './app.component'
import { CompanyHierarchyComponent } from './components/company-hierarchy/company-hierarchy.component'

export const appRoutes: Routes = [
    { 
      path: '',
      redirectTo: '/home',
      pathMatch: 'full'
    },  
    { 
      path: 'home', 
      component: CompanyHierarchyComponent 
    }
  ];