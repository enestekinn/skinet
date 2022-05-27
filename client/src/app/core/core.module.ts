import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';



@NgModule({
  declarations: [NavBarComponent],
  imports: [
    CommonModule
  ],
  // navbar com.  core in altina koyduktan sonra burada tanimladik.
  exports: [NavBarComponent]
})
export class CoreModule { }
