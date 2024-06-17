import { AuthService } from 'src/app/core/services/auth.service';
import { CustomerService } from './../../services/customer.service';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, catchError, debounceTime, distinctUntilChanged, of, tap } from 'rxjs';
import { ICustomerResponse } from '../../models/Response/ICustomerResponse';
import { IPageResponse } from '../../models/Response/IPageResponse';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  formBuilder: FormBuilder = inject(FormBuilder);
  customerService: CustomerService = inject(CustomerService);
  authService: AuthService = inject(AuthService);
  router: Router = inject(Router);
  customers: Observable<IPageResponse<ICustomerResponse>> = new Observable<IPageResponse<ICustomerResponse>>();
  getCustomerParameters = {
    cpf: null,
    name: null,
    pageSize: 5,
    pageNumber: 1
  }

  searchForm: FormGroup = this.formBuilder.group({
    cpf: ["", Validators.required],
    name: ["", Validators.required]
  });

  constructor() {
    this.searchForm.valueChanges.pipe(
      debounceTime(1000),
      distinctUntilChanged(),
      tap((data) => {
        this.getCustomerParameters.cpf = data.cpf;
        this.getCustomerParameters.name = data.name;
        this.getCustomers();
      })
    ).subscribe();
  }

  ngOnInit(): void {
    this.getCustomers();
  }


  getCustomers() {
    this.customerService.getCustomers(this.getCustomerParameters).pipe(
      tap((data) => this.customers = of(data))
    ).subscribe()
  }

  navigateToHomePage() {
    this.router.navigate(["/"]);
  };

  onDelete(id: number) {
    if(confirm("would you like to delete this customer?")) {
      this.customerService.deleteCustomer(id).pipe(
        catchError((error: string) => {
          console.log(error);
          return of();
        }),
        tap(() => this.getCustomers()),
      ).subscribe();
    };
  }

  onLogout() {
    this.navigateToHomePage();
    this.authService.removeToken("token");
  }

}
