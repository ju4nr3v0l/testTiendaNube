import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthCallback } from './components/auth-callback/auth-callback';
import { Dashboard } from './components/dashboard/dashboard';
import { Payments } from './components/payments/payments';

const routes: Routes = [
  { path: 'auth/callback', component: AuthCallback },
  { path: 'dashboard', component: Dashboard },
  { path: 'payments', component: Payments },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
