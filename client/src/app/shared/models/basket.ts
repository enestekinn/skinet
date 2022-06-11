import {v4 as uuidv4} from 'uuid'
export interface IBasket {
    id: string;
    items: IBasketItem[];
  clientSecret?: string;
  paymentIntentId?: string;
  deliveryMethodId?: number;
  shippingPrice?: number;
}

    export interface IBasketItem {
        id: number;
        productName: string;
        price: number;
        quantity: number;
        pictureUrl: string;
        brand: string;
        type: string;
    }

 // we want uniqiue identifier when we create a new instance of this particular basket
export class Basket implements IBasket {
    // uuidv4 icin paket yukledik
    id = uuidv4();
    items: IBasketItem[] = []; //empty array

}

export interface IBasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}

