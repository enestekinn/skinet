import { NgModule } from '@angular/core';
import { ShopComponent } from './shop.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  {path: '' , component: ShopComponent}, //empty string is routes component for our shop module
  {path: ':id', component: ProductDetailsComponent},
];

@NgModule({
  declarations: [],
  imports: [
    // app modulde forRoot kullandik  fakat burada forChild ile sadec shop modulde mevcut olacak
    RouterModule.forChild(routes) 
  ],

  // we want to use router module inside our shop module 
  exports: [RouterModule]
})
export class ShopRoutingModule { }
