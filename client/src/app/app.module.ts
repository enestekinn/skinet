import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { ShopModule } from './shop/shop.module';
import { HomeModule } from './home/home.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import {NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import {ReactiveFormsModule} from "@angular/forms";
import {SharedModule} from "./shared/shared.module";
import {JwtInterceptor} from "./core/interceptors/jwt.interceptor";
import { OrdersComponent } from './orders/orders.component';

@NgModule({
  declarations: [
    // yeni component ekledigimizde buraya ekliniyor
    AppComponent,
    LoginComponent,
    RegisterComponent,

  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        HttpClientModule,
        CoreModule,
        //  ShopModule, bunu kaldirdik cunku uygulama ilk acildiginda bu compoennt in yuklenmesini istemiyoruz.
        FontAwesomeModule,
        HomeModule,
        NgxSpinnerModule,
        ReactiveFormsModule,
        SharedModule
    ],
  providers: [

    // yazdigimiz servisleri burada da tanimliyoruz
    {provide: HTTP_INTERCEPTORS,useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS,useClass: LoadingInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS,useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

// Navbari CoreModule altina koyduk onu calistirmak icin Navbari buradan sildik CoreModulu ekledik.
