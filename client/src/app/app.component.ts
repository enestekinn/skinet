import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import {AccountService} from "./account/account.service";



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

// Buradan deger veriyoruz bu degeri  html den cekiyoruz.
export class AppComponent  implements OnInit{

  title = 'Skinet';


// uygulama ilk acildiginda sepetteki urunleri getiricez startup file
  constructor(private basketService: BasketService,private accountService: AccountService){

  }

  ngOnInit(): void {
    this.loadingCurrentUser();
this.loadBasket();
  }

  loadingCurrentUser(){
    const  token = localStorage.getItem('token');

      this.accountService.loadCurrentUser(token).subscribe(() => {
        console.log('loaded user');
      },error => {
        console.log(error);
      });

  }

  loadBasket(){
    const basketId = localStorage.getItem('basket_id');
    if(basketId){
      this.basketService.getBasket(basketId).subscribe(() => {
        console.log('initialised basket');
      }, error => {
        console.log(error);
      })
    }
  }
}
