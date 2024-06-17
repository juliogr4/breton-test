import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { AddEditCustomerComponent } from './pages/add-edit-customer/add-edit-customer.component';
import { CustomerListComponent } from './pages/customer-list/customer-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AddEditCustomerComponent,
    CustomerListComponent
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ]
})
export class CustomerModule { }
