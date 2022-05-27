import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component'


@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot()
  ],
  // Paginationi baska yerlerde kullanmak icin burada export ediyoruz.
  exports: [
    PaginationModule,
  PagingHeaderComponent,
PagerComponent]
})
export class SharedModule { }


/* 
forRoot()
the pagination module has its own provider's array and those providers need to be injected into
our route module and startup paginationi  ngx-bootstrap kutuphanesi ile aldik.
 */