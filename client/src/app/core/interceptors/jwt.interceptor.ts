import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import { Injectable} from '@angular/core';
import {Observable} from "rxjs";

@Injectable()
export class JwtInterceptor implements  HttpInterceptor{
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const token = localStorage.getItem('token');
      if (token){
        req = req.clone({
            setHeaders: {
              Authorization: `Bearer ${token}`
            }
          });
      }
    return next.handle(req);
  }

}
/*fetch our token from local storage if we have a token then we want it to automatically set the head is inside any request there's
going to Api server
* */
