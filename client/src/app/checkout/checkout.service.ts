import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";
import {IDeliveryMethod} from "../shared/models/deliveryMethod";
import {IOrderToCreate} from "../shared/models/order";

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor(private  http: HttpClient) { }


  createOrder(order: IOrderToCreate){
    return this.http.post(this.baseUrl + 'orders',order);
  }

  getDeliveryMethod(){
    return this.http.get(this.baseUrl+'orders/deliveryMethod').pipe(
      map((dm: IDeliveryMethod[]) => {
       return dm.sort((a,b) => b.price - a.price);
    })
    );
  }
}
