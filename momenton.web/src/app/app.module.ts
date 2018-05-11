import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { appRoutes } from './routerConfig';

import { AppComponent } from './app.component';
import { CompanyHierarchyComponent } from './components/company-hierarchy/company-hierarchy.component';


@NgModule({
  declarations: [
    AppComponent,
    CompanyHierarchyComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent, CompanyHierarchyComponent]
})
export class AppModule { }
