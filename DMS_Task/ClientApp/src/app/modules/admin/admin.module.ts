import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AdminRoutingModule } from './admin-routing.module';
import { HomeComponent } from './components/home/home.component';
import { SideMenuComponent } from './components/side-menu/side-menu.component';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap"
import { ChartModule } from 'primeng/chart';
import { UnitOfMeasureComponent } from './components/unit-of-measure/unit-of-measure.component';
import { AddUnitOfMeasureComponent } from './components/add-unit-of-measure/add-unit-of-measure.component';
import { EditUnitOfMeasureComponent } from './components/edit-unit-of-measure/edit-unit-of-measure.component';
import { ItemsComponent } from './components/items/items.component';
import { AddItemComponent } from './components/add-item/add-item.component';
import { EditItemComponent } from './components/edit-item/edit-item.component';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { FileUploadModule } from 'primeng/fileupload';
import { ToastrModule } from 'ngx-toastr';


@NgModule({
  declarations: [
    HomeComponent,
    SideMenuComponent,
    UnitOfMeasureComponent,
    AddUnitOfMeasureComponent,
    EditUnitOfMeasureComponent,
    ItemsComponent,
    AddItemComponent,
    EditItemComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    ChartModule,
    ProgressSpinnerModule,
    FileUploadModule,
    ToastrModule
  ]
})
export class AdminModule { }
