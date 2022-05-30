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

@NgModule({
  declarations: [
    // yeni component ekledigimizde buraya ekliniyor
    AppComponent,
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
    NgxSpinnerModule
  ],
  providers: [

    // yazdigimiz servisleri burada da tanimliyoruz
    {provide: HTTP_INTERCEPTORS,useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS,useClass: LoadingInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

// Navbari CoreModule altina koyduk onu calistirmak icin Navbari buradan sildik CoreModulu ekledik.
