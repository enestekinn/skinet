import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import {Basket, IBasket, IBasketItem, IBasketTotals} from '../shared/models/basket';
import { IProduct } from '../shared/models/product';

// $ icon means this is observable
@Injectable({
  providedIn: 'root'
})
export class BasketService {
baseUrl = environment.apiUrl;

//BehaviorSubject emits its current value whenever it is subscribed to
private basketSource = new BehaviorSubject<IBasket>(null);
basket$ = this.basketSource.asObservable();

private  basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }



getBasket(id: string){

  return this.http.get(this.baseUrl + 'basket?id='+id)
  .pipe(
    map((basket: IBasket) => {
      this.basketSource.next(basket);
      this.calculateTotals();
    })
  );
}


// that's going to update our behavior subject with the new value
setBasket (basket: IBasket){
  return this.http.post(this.baseUrl + 'basket',basket).subscribe((response: IBasket) =>{
    this.basketSource.next(response);
    this.calculateTotals()
  }, error =>{
    console.log(error);
  });
}


getCurrentBasketValue(){
  return this.basketSource.value;
}


// convert product model to basket
addItemToBasket(item: IProduct, quantity =1){
  const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item,quantity);
  //we don't know user have basket or already got a basket
  const basket = this.getCurrentBasketValue() ?? this.createBasket() // if getCurrentBasketValue null
  basket.items= this.addOrUpdateItem(basket.items,itemToAdd,quantity);
  this.setBasket(basket)
}
incrementItemQuantity(item: IBasketItem){
    const  basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex(x => x.id === item.id)
  basket.items[foundItemIndex].quantity++;
    this.setBasket(basket);
}
  decrementItemQuantity(item: IBasketItem){
    const  basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex(x => x.id === item.id)
    if (basket.items[foundItemIndex].quantity > 1){
      basket.items[foundItemIndex].quantity--;
    }else {
      this.removeItemFromBasket(item);
    }
    this.setBasket(basket);
  }

  removeItemFromBasket(item: IBasketItem) {
const basket = this.getCurrentBasketValue();
if (basket.items.some( x=> x.id === item.id)){
  basket.items = basket.items.filter(i => i.id !== item.id);
  if (basket.items.length > 0){
    this.setBasket(basket);
  }else {
    this.deleteBasket(basket);
  }
}
  }

  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl+ 'basket?id='+basket).subscribe(() =>{
      this.basketSource.next(null);
      this.basketSource.next(null);
      localStorage.removeItem('basket_id')
    },error => {
      console.log(error);
    });
  }

private  calculateTotals(){
    const basket = this.getCurrentBasketValue();
    const shipping = 0;
    const subtotal = basket.items.reduce((a,b) => (b.price * b.quantity) + a ,0 );
    const total = subtotal + shipping;
    this.basketTotalSource.next({shipping,total,subtotal})
}


// user bir urun ekledi eger o urun sepette yoksa  ekeleriz varsa sayaci quantitysini eklemeliyiz.
  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
const index = items.findIndex(i => i.id === itemToAdd.id);
if(index === -1){ // urun sepette degilse urunu ekliyoruz burda
  itemToAdd.quantity = quantity
  items.push(itemToAdd)
}else {// sepette varsa bir arttiriyoruz
  items[index].quantity += quantity
}
return items;
  }


// basket id yaratildiginda onu  localstorage a koyuyoruz.
  createBasket(): IBasket {
const basket = new Basket();
localStorage.setItem('basket_id',basket.id);
return basket;
  }


  private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,
      brand: item.productBrand,
      type: item.productType
    };
  }


}
/*
what we want to do is from the response that we get back from Htp client , which should contain our basket
we need to set our basket source  with the basket we get back from the API and we we'll do to achieve this is we'll use pipe


*/

