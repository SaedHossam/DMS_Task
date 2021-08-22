import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeProfileComponent } from './components/employee-profile/employee-profile.component';
import { ProfileComponent as CompanyProfile } from "../company/components/profile/profile.component";


const routes: Routes = [
  { path: 'profile/public', component: EmployeeProfileComponent },
  { path: 'profile/:id', component: EmployeeProfileComponent },
  { path: 'company/:id', component: CompanyProfile},
  { path: '', redirectTo: 'jobs', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
