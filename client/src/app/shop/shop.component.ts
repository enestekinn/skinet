import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brands';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  // our search field input is a child of our shop component  we are access #search
  /*
  static default value is false  our input element  dont depend on  anything else
  we dont put any conditions here to decide whether or not to show it then we can set the static property is equal to 'true' 
   */
  //@ViewChild('search',{static: true}) searchTerm: ElementRef;
  // bunun staticligini kaldirdik progress bar   ciktiginda buna erisemiyoruz halbuki statik degismemisi lazim 
  @ViewChild('search',{static: false}) searchTerm: ElementRef;
  products: IProduct [];
  brands: IBrand [];
  types: IType[];
  //we want to store what type of id is selected
  // site ilk acildiginda All kategorisi secili olacak 0 o yuzden
  // bunlari yeni olusturdugumuz classa gonderdik.
  //brandIdSelected: number = 0;
  //typeIdSelected: number = 0;
  //sortSelected = 'name';
  // value degerleri harfi harfine yazilmali.
  shopParams = new ShopParams();
  totalCount: number = 0
  sortOptions =[
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ];


  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
this.getProducts();
this.getBrands();
this.getTypes();
  }


getProducts(){
  this.shopService.getProducts(this.shopParams).subscribe(response => {
    this.products = response.data;
    this.shopParams.pageNumber = response.pageIndex;
    this.shopParams.pagesize = response.pageSize;
    this.totalCount = response.count;
    
  }, error => {

    console.log(error);
  })
}

getBrands(){
  this.shopService.getBrands().subscribe(response => {
    //  tum brand lari alip All isimli listeye ediyor.
    this.brands = [{id: 0, name: 'All'}, ...response];
  },error =>{
    console.log(error);
  })
}
getTypes(){
  this.shopService.getTypes().subscribe(response =>{
    //  tum types lari alip All isimli listeye ediyor.
    this.types = [{id: 0, name: 'All'}, ...response];
  }, error => {
    console.log(error);
  })
}

onBrandSelected(brandId: number){
  this.shopParams.brandId = brandId;
  this.getProducts();
}
onTypeSelected(typeId: number){
  this.shopParams.typeId = typeId;
  this.shopParams.pageNumber =1;
  this.getProducts();
}
onSortSelected(sort: string){
  this.shopParams.sort = sort;
  this.getProducts();
}
onPageChanged(event: any){
  if(this.shopParams.pageNumber !== event){
    this.shopParams.pageNumber = event;
    this.getProducts();
  }
 
}

onSearch(){
  this.shopParams.search = this.searchTerm.nativeElement.value
  this.shopParams.pageNumber =1;
  this.getProducts();
}
onReset(){
  this.searchTerm.nativeElement.value = ''
  this.shopParams = new ShopParams();
  this.getProducts();
}
}
