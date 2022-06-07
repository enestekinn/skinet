import {Component, Input, OnInit} from '@angular/core';
import {CdkStepper} from "@angular/cdk/stepper";

@Component({
  selector: 'app-stepper',
  templateUrl: './stepper.component.html',
  styleUrls: ['./stepper.component.scss'],
  // extend yaptiktan sonra bunuda yazdik
  providers: [{provide: CdkStepper,useExisting: StepperComponent}]
})

// extends  give us access to the  cdkstepper functionality
export class StepperComponent  extends CdkStepper implements OnInit {
  @Input() linearModeSelected: boolean;



  ngOnInit(): void {
    this.linear = this.linearModeSelected;
  }

  onClick(index: number){
    this.selectedIndex = index;
  }

}
