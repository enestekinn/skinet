import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { ShopComponent } from './shop/shop.component';


const routes: Routes = [
  {path: '', component: HomeComponent},
  //{path: 'shop' , component: ShopComponent},
  {path: 'shop' ,loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule)},
  {path: 'shop/:id', component: ProductDetailsComponent},// navigate yaparken id bu sekilde gonderiyoruz
  //pathMatch  matches against the entire url and it is important to do this when we redirecting empty path route
  // we need this when we are trying to  redirect sb who typed in bad url will use this particular route to get them back to the home page 
  {path: '**', redirectTo: '',pathMatch: 'full'} // this is  default page 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
