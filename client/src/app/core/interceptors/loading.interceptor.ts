import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { BusyService } from '../services/busy.service';
import { delay, finalize } from 'rxjs/operators';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.method === 'POST' && request.url.includes('orders')) {
      return next.handle(request);
    }
    // making spesific request sign up ekraninda email adres kullanimda mi degil mi kontorlu yaparken spinner cikmasini istemiyoruz.
    if (request.url.includes('emailexists')) {
      return next.handle(request);
    }
    this.busyService.busy();
    return next.handle(request).pipe(
      delay(1000),
      finalize(() => {
        this.busyService.idle();
      })
    );
  }
}
