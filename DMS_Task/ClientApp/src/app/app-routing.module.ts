import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from "./error-pages/not-found/not-found.component";
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { HomeComponent } from './home/home.component';
import { AdminGuard } from './shared/guards/admin.guard';
import { AuthGuard } from './shared/guards/auth.guard';
import { CompanyGuard } from "./shared/guards/company.guard";
import { EmployeeGuard } from "./shared/guards/employee.guard";
import { EmployerRegisterComponent } from "./employer-register/employer-register.component";
import { InternalServerComponent } from './error-pages/internal-server/internal-server.component';
import { SearchItemsComponent } from './search-items/search-items.component';
import { ItemDetailsComponent } from './item-details/item-details.component';
import { CartComponent } from './cart/cart.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'authentication', loadChildren: () => import("../app/authentication/authentication.module").then(m => m.AuthenticationModule) },
  { path: 'admin', loadChildren: () => import("./modules/admin/admin.module").then(m => m.AdminModule), canActivate: [AuthGuard, AdminGuard] },
  { path: 'company', loadChildren: () => import("./modules/company/company.module").then(m => m.CompanyModule), canActivate: [AuthGuard, CompanyGuard] },
  { path: 'employee', loadChildren: () => import("./modules/employee/employee.module").then(m => m.EmployeeModule), canActivate: [AuthGuard, EmployeeGuard] },
  { path: 'forbidden', component: ForbiddenComponent },
  { path: 'employer-register', component: EmployerRegisterComponent },
  { path: 'search-items/:term', component: SearchItemsComponent },
  { path: 'item/:id', component: ItemDetailsComponent },
  { path: 'cart', component: CartComponent },
  { path: '404', component: NotFoundComponent },
  { path: '500', component: InternalServerComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/404', pathMatch: 'full' },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
