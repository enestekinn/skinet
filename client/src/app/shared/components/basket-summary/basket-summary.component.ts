import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Observable} from "rxjs";
import {IBasket, IBasketItem} from "../../models/basket";
import {BasketService} from "../../../basket/basket.service";
import {IOrderItem} from "../../models/order";

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent implements OnInit {
basket$: Observable<IBasket>
  @Output() decrement: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() increment: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() remove: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  //review kisimda ekleme cikar silme islemlerini kaldiriyoruz
  @Input() isBasket = true;
  @Input() items: IBasketItem[] | IOrderItem[] = [];
  @Input() isOrder = false;


  constructor() { }

  ngOnInit(): void {

  }
  decrementItemQuantity(item: IBasketItem){
    this.decrement.emit(item)
  }
  incrementItemQuantity(item: IBasketItem){
    this.increment.emit(item)
  }
  removeBasketItem(item: IBasketItem){
    this.remove.emit(item)
  }

}
