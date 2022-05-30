import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component'
import {CarouselModule} from 'ngx-bootstrap/carousel';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component'


@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot()
  ],
  // Paginationi baska yerlerde kullanmak icin burada export ediyoruz.
  exports: [
    PaginationModule,
  PagingHeaderComponent,
PagerComponent,
CarouselModule,
  OrderTotalsComponent]
})
export class SharedModule { }


/*
forRoot()
the pagination module has its own provider's array and those providers need to be injected into
our route module and startup paginationi  ngx-bootstrap kutuphanesi ile aldik.
 */
