import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';
import {BasketService} from "../../basket/basket.service";

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;

  //activateRoute rootdaki parametreye ulasmak icin kullaniyoruz.
  constructor(
    private shopService: ShopService,
    private activateRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService
     ) {
       // progress bar calistiginda arka planda urunun adi geliyor onu burada empty yapiyoruz.
       this.bcService.set('@productDetails',' ');
      }

  ngOnInit(): void {
    this.loadProduct();

  }

  addItemToBasket(){
    this.basketService.addItemToBasket(this.product,this.quantity);
  }
  incrementQuantity(){
    this.quantity++;
  }
  decrementQuantity(){
    if (this.quantity > 1){
      this.quantity--;

    }
  }

  loadProduct(){
    // +  ifadeyi string e cevirmek icin
    // id ismini daha once router da belirledik.

    this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(product => {
      this.product = product;
      this.bcService.set('@productDetails',product.name)
    },
    error =>{
console.log(error);
    })
  }

}


// Hard Coding Ornegi

/*
export class ProductDetailsComponent implements OnInit {
  product: IProduct

  //dependency injection
  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    this.shopService.getProduct(2).subscribe(product => {
      this.product = product
    },
    error =>{
console.log(error);
    })
  }

}
*/
