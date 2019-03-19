import { Observable, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse) {
          //Unaurhroized errors
          if (error.status === 401) {
            return throwError(error.statusText);
          }
          //Bad Request Errors or thrown exceptions on the server
          const applicationError = error.headers.get('Application-Error');
          if (applicationError ) {
            return throwError(applicationError);
          }

          //Model state errors
          const serverError = error.error;
          let modelStateError = '';
          if (serverError && typeof( serverError) === 'object') {
            for (const key in serverError) {
              if (serverError[key]) {
                modelStateError += serverError[key] + '\n';
              }
            }
          }
          return throwError(serverError || modelStateError || 'Server Error');
        }
      })
    );
  }
}
export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};
