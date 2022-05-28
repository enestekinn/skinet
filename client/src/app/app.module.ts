import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';  
import { ShopModule } from './shop/shop.module';
import { HomeModule } from './home/home.module';

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
    HomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

// Navbari CoreModule altina koyduk onu calistirmak icin Navbari buradan sildik CoreModulu ekledik.
