import { Injectable } from '@angular/core';
import {HttpEvent, HttpHandler, HttpHandlerFn, HttpInterceptor, HttpRequest} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('jwtToken'); // Get the token from local storage

    if (!token) {
      return next(req);
    }

    const req1 = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`),
    });

    return next(req1);
  }
}
