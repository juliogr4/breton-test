import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerListComponent } from './pages/customer-list/customer-list.component';
import { AddEditCustomerComponent } from './pages/add-edit-customer/add-edit-customer.component';

const routes: Routes = [
  { path: "", component: CustomerListComponent },
  { path: "add-customer", component: AddEditCustomerComponent },
  { path: "edit-customer/:id", component: AddEditCustomerComponent },
  { path: "", pathMatch: "full", redirectTo: "" },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
