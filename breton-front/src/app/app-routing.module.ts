import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

const routes: Routes = [
  {
    path: "account",
    loadChildren: () => import("./features/account/account.module").then(m => m.AccountModule)
  },
  {
    path: "customer",
    loadChildren: () => import("./features/customer/customer.module").then(m => m.CustomerModule),
    canActivate: [authGuard]
  },
  { path: "", redirectTo: "account", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
