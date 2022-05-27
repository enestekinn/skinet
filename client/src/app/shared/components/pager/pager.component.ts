import { Component, Input, OnInit, Output,EventEmitter} from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
  @Input() totalCount: number;
  @Input() pageSize: number; // Input we receive from parent component 

  // Output Component is a way that a child component is going to be a child component on the shop components page.
  // we want to make call from child component
  @Output() pageChanged = new EventEmitter<number>();  

  // be careful there are two import for EventEmitter 

  constructor() { }

  ngOnInit(): void {
  }

  onPagerChange(event: any){
    // we are emitting from pagination component
    this.pageChanged.emit(event.page);
  }

}
