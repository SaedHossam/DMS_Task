import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeRoutingModule } from './employee-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CardModule, } from 'primeng/card';
import { RippleModule } from 'primeng/ripple';
import { SkeletonModule } from 'primeng/skeleton';
import { ToastrModule } from 'ngx-toastr';
import { EmployeeProfileComponent } from './components/employee-profile/employee-profile.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MultiSelectModule } from 'primeng/multiselect';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CalendarModule } from 'primeng/calendar';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { FileUploadModule } from 'primeng/fileupload';
import { OrdersComponent } from './orders/orders.component';
import { OrderDetailsComponent } from './order-details/order-details.component';

@NgModule({
  declarations: [
    EmployeeProfileComponent,
    OrdersComponent,
    OrderDetailsComponent,
  ],

  imports: [
    CommonModule,
    EmployeeRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    CardModule,
    RippleModule,
    SkeletonModule,
    NgbModule,
    MultiSelectModule,
    AutoCompleteModule,
    CalendarModule,
    ProgressSpinnerModule,
    FileUploadModule,
    ToastrModule.forRoot(),

  ]
})
export class EmployeeModule { }
