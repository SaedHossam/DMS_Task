import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CalendarModule } from 'primeng/calendar';
import { PasswordModule } from 'primeng/password';
import { ChipsModule } from 'primeng/chips';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { EditorModule } from 'primeng/editor';
import { SelectButtonModule } from 'primeng/selectbutton';
import { RippleModule } from 'primeng/ripple';
import { InputNumberModule } from 'primeng/inputnumber';
import { MultiSelectModule } from 'primeng/multiselect';
import { FileUploadModule } from 'primeng/fileupload';
import { CompanyRoutingModule } from './company-routing.module';
import { HomeComponent } from './components/home/home.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@NgModule({
  declarations: [
    HomeComponent,
    ProfileComponent,
  ],
  imports: [
    CommonModule,
    CompanyRoutingModule,
    FormsModule,
    CalendarModule,
    PasswordModule,
    ReactiveFormsModule,
    ChipsModule,
    AutoCompleteModule,
    EditorModule,
    SelectButtonModule,
    RippleModule,
    InputNumberModule,
    NgbModule,
    MultiSelectModule,
    FileUploadModule,
    ConfirmDialogModule,
    ProgressSpinnerModule
  ]
})
export class CompanyModule { }
