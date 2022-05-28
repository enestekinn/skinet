import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct

  //activateRoute rootdaki parametreye ulasmak icin kullaniyoruz.
  constructor(private shopService: ShopService,private activateRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    // +  ifadeyi string e cevirmek icin  
    // id ismini daha once router da belirledik.
    
    this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(product => {
      this.product = product
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