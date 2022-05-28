import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import  {IBrand} from '../shared/models/brands';
import { IType } from '../shared/models/productType';
import { map } from 'rxjs/operators'; // rxjs calismazsa bunu import et 
import { ShopParams } from '../shared/models/shopParams';
import { IProduct } from '../shared/models/product';

@Injectable({
  // root refer to app module
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/'

  constructor(private http: HttpClient) { }

//{{url}}/api/products?typeId=3&brandId=2
  //filtreleme icin 2 tane parametre koyduk
  getProducts(shopParams: ShopParams){

    // param nesnesi objesi olusrup query string olarak api endpoint e gonderiyoruz.
    let params = new HttpParams();
    // brand id miz varsa query string e ekliyoruz.
    if(shopParams.brandId !== 0){
      // brandId (querty stirng) , brandId.toString() (value)
      params = params.append('brandId',shopParams.brandId.toString())
    }
    if(shopParams.typeId !== 0){
      params = params.append('typeId',shopParams.typeId.toString())
    }
    if(shopParams.search){
      params = params.append('search',shopParams.search);
    }
    
    
      params = params.append('sort',shopParams.sort);


      params = params.append('pageIndex',shopParams.pageNumber.toString());
      params = params.append('pageIndex',shopParams.pagesize.toString());
    
    // observing response giving us http respond instead of body of request we have to extract the body out of this.
    
    return this.http.get<IPagination>(this.baseUrl + 'products',{observe: 'response',params})
    .pipe(
    map(response => {
      return response.body; // response.body = IPagination object
    })
    );
  }


  // product details sayfasinda gostermek icin yeni method olusturuyoruz.
  getProduct(id: number){
return this.http.get<IProduct>(this.baseUrl + 'products/'+ id);
  }

  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }
  getTypes(){
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }

}
