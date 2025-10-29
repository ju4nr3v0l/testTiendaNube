import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { AuthCallback } from './components/auth-callback/auth-callback';
import { Dashboard } from './components/dashboard/dashboard';
import { Payments } from './components/payments/payments';
import { userAgentInterceptor } from './interceptors/user-agent-interceptor';

@NgModule({
  declarations: [
    App,
    AuthCallback,
    Dashboard,
    Payments
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptors([userAgentInterceptor]))
  ],
  bootstrap: [App]
})
export class AppModule { }
