import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';
import {AuthGuard} from "./core/guards/auth.guard";



const routes: Routes = [
  {path: '', component: HomeComponent,data: {breadcrumb: 'Home' }},
  {path: 'test-error', component: TestErrorComponent, data: {breadcrumb: 'Test Errors'}},
  {path: 'server-error', component: ServerErrorComponent, data: {breadcrumb: 'Server Errors'}},
  {path: 'not-found', component: NotFoundComponent, data: {breadcrumb: 'Not Found'}},
  //{path: 'shop' , component: ShopComponent},
  {path: 'shop' ,loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule),
  data: {breadcrumb: 'Shop '}},

// yeni modul olusturduk burada belirtiyoruz.
{path: 'basket' ,loadChildren: () => import('./basket/basket.module').then(mod => mod.BasketModule),
data: {breadcrumb: 'Basket '}},

  {
    path: 'checkout' ,
    canActivate: [AuthGuard],
    loadChildren: () => import('./checkout/checkout.module').then(mod => mod.CheckoutModule),
    data: {breadcrumb: 'Checkout'}},

  {path: 'account' ,loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule),
    data: {breadcrumb: {skip: true}}},
 // {path: 'shop/:id', component: ProductDetailsComponent},// navigate yaparken id bu sekilde gonderiyoruz
  //pathMatch  matches against the entire url and it is important to do this when we redirecting empty path route
  // we need this when we are trying to  redirect sb who typed in bad url will use this particular route to get them back to the home page
  {path: '**', redirectTo: 'not-found',pathMatch: 'full'} // this is  default page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
