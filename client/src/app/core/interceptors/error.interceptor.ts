import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, delay, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable() // if we don't make this injectable, then it's never able to be utilized and will never handle our erros.
export class ErrorInterceptor implements HttpInterceptor {
constructor(private router: Router,private toastr: ToastrService){

}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      delay(1000),
      catchError(error => {
        if(error){
          if(error.status === 400){
            console.log(error.error.errors+ " enes")
            if(error.error.errors){
              throw error.error; // if it is a validation error, we are throwing it back to the component
            }else {
              this.toastr.error(error.error.message,error.error.statusCode)

            }
          }
          if(error.status === 401){
            this.toastr.error(error.error.message,error.error.statusCode)
        }
          if(error.status === 404){
            this.router.navigateByUrl('/not-found');
          }
          if(error.status === 500){

            // navigate yapmadan once route a state gonderiyoruz.
            /* kisaca api mizde error: {details ile baslayan yeri diziyi component a gondermeye calisiyoruz. } */
            const navigationExtras: NavigationExtras = {state : {error: error.error}};
            this.router.navigateByUrl('/server-error',navigationExtras);
          }
        }
        return throwError(error);


      })
    );
  }
}
