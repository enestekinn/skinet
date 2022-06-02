import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {AccountRoutingModule} from "./account-routing.module";
import {SharedModule} from "../shared/shared.module";



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AccountRoutingModule,
    // Shared module deki exportlari almak icin burda declera ediyoruz.
    SharedModule
  ]
})
export class AccountModule { }
