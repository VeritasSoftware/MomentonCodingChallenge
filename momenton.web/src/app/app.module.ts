import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { CompanyHierarchyComponent } from './components/company-hierarchy/company-hierarchy.component';


@NgModule({
  declarations: [
    AppComponent,
    CompanyHierarchyComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
