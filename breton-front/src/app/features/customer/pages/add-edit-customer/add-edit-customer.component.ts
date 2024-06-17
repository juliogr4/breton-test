import { CustomerService } from './../../services/customer.service';
import { Component, OnDestroy, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, catchError, debounceTime, distinctUntilChanged, map, of, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-add-edit-customer',
  templateUrl: './add-edit-customer.component.html',
  styleUrls: ['./add-edit-customer.component.css']
})
export class AddEditCustomerComponent implements OnInit, OnDestroy {
  formBuilder : FormBuilder = inject(FormBuilder);
  activedRoute : ActivatedRoute = inject(ActivatedRoute);
  router: Router = inject(Router);
  customerService : CustomerService = inject(CustomerService);

  unsubscribe: Subscription = new Subscription();

  customerForm: FormGroup = this.formBuilder.group({
    id: ['', Validators.required],
    createdBy: [{ value: '1', disabled: true }, Validators.required],
    name: ['', Validators.required],
    cpf: ['', Validators.required],
    birthDate: ['', Validators.required],
    phone: ['', Validators.required],
    action: ['Create', Validators.required],
    address: this.formBuilder.group({
      postalCode: ['', Validators.required],
      street: [{ value: '', disabled: true }, Validators.required],
      complement: [{ value: '', disabled: true }, Validators.required],
      neighborhood: [{ value: '', disabled: true }, Validators.required],
      city: [{ value: '', disabled: true }, Validators.required],
      state: [{ value: '', disabled: true }, Validators.required],
      ibge: [{ value: '', disabled: true }, Validators.required],
      gia: [{ value: '', disabled: true }, Validators.required],
      ddd: [{ value: '', disabled: true }, Validators.required],
      siafi: [{ value: '', disabled: true }, Validators.required]
    }),
  })

  constructor() {
    this.customerForm.get("address")?.get("postalCode")?.valueChanges.pipe(
      debounceTime(1000),
      distinctUntilChanged(),
      switchMap(postal => {
        return this.customerService.getAddressByPostalCode(postal).pipe(
          catchError(error => {
            console.error(error);
            return of(null);
          })
        );
      })
    ).subscribe((address) => {
      if (address) {
        this.customerForm.get("address")?.patchValue({
          street: address.street,
          complement: address.complement,
          neighborhood: address.neighborhood,
          city: address.city,
          state: address.state,
          ibge: address.ibge,
          gia: address.gia,
          ddd: address.ddd,
          siafi: address.siafi
        });
      }
    });
  }

  get action(): string {
    return this.customerForm.get("action")?.value;
  }

  navigateToHomePage() {
    this.router.navigate(["/"]);
  };

  ngOnInit(): void {
    this.unsubscribe = this.activedRoute.params.pipe(
      map(param => param["id"]),
      switchMap((customerId: number | undefined) => {
        if(!!customerId) {
          return this.customerService.getCustomerById(customerId).pipe(
            catchError((error) => {
              this.navigateToHomePage();
              return of();
            }),
            tap((customer) => {
              const { address, birthDate, ...customerWithoutDate } = customer;

              this.customerForm.patchValue(customerWithoutDate);

              this.customerForm.patchValue({
                birthDate: new Date(birthDate).toISOString().split('T')[0],
                action: "Edit"
              });

              this.customerForm.get("address")?.patchValue(address);

              this.customerForm.get('id')?.disable();

            }),
          )
        } else {
          this.customerForm.get('id')?.disable();
          this.customerForm.get('createdBy')?.disable();

          this.customerForm.get('id')?.setValidators(null);
          this.customerForm.get('createdBy')?.setValidators(null);
        }
        return of();
      })
    ).subscribe();
  }


  onSave() {
    const data = this.customerForm.getRawValue();
    // if(!data.valid || !data.address.valid) {
    //   alert("All fieds must be filled out");
    //   return;
    // };

    console.log(this.customerForm.getRawValue())

    const operation = this.action.toLowerCase() === 'edit' ?
      this.customerService.updateCustomer(this.customerForm.getRawValue()):
      this.customerService.createCustomer(this.customerForm.getRawValue());

    operation.pipe(
      catchError((error: string) => {
        console.log(error)
        return of();
      }),
      tap(response => {
        alert(`customer ${this.action}ed successfully`)
        this.navigateToHomePage()
      }),
    ).subscribe();
  }

  ngOnDestroy(): void {
    this.unsubscribe.unsubscribe();
  };

}
