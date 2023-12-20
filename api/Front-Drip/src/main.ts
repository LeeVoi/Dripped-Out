/*
*  Protractor support is deprecated in Angular.
*  Protractor is used in this example for compatibility with Angular documentation tools.
*/
import { bootstrapApplication,provideProtractorTestingSupport } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import {HTTP_INTERCEPTORS, provideHttpClient, withInterceptors, withJsonpSupport} from "@angular/common/http";
import { provideRouter} from "@angular/router";
import routeConfig from './app/routes';
import {AuthInterceptor} from "./app/AuthInterceptor";

bootstrapApplication(AppComponent,
  {providers: [
      provideProtractorTestingSupport(),
      provideRouter(routeConfig),
      provideHttpClient(withJsonpSupport(), withInterceptors([(req, next) => new AuthInterceptor().intercept(req, next)])),
    ]})
  .catch(err => console.error(err));
