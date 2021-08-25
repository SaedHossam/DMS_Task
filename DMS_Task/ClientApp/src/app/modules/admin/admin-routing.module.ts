import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddItemComponent } from './components/add-item/add-item.component';
import { AddUnitOfMeasureComponent } from './components/add-unit-of-measure/add-unit-of-measure.component';
import { EditItemComponent } from './components/edit-item/edit-item.component';
import { EditUnitOfMeasureComponent } from './components/edit-unit-of-measure/edit-unit-of-measure.component';
import { HomeComponent } from "./components/home/home.component";
import { ItemsComponent } from './components/items/items.component';
import { UnitOfMeasureComponent } from './components/unit-of-measure/unit-of-measure.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'unit-of-measure', component: UnitOfMeasureComponent },
  { path: 'add-unit-of-measure', component: AddUnitOfMeasureComponent },
  { path: 'edit-unit-of-measure/:id', component: EditUnitOfMeasureComponent },
  { path: 'items', component: ItemsComponent },
  { path: 'add-item', component: AddItemComponent },
  { path: 'edit-item/:id', component: EditItemComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
