import {Component, ElementRef, Input, OnInit, Self, ViewChild} from '@angular/core';
import {ControlValueAccessor, NgControl} from "@angular/forms";

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements OnInit, ControlValueAccessor {
@ViewChild('input',{static: true}) input: ElementRef;
@Input() type ='text';
@Input() label:string;

// in order to access the validaiton we need to get access to the control itself the way we can do it
  constructor(@Self() public  controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
/*
    why we are getting validators from the control object, then setting them back to the same control object.
We do not know if we have any validators set for the control so we want to set it to an empty array if that is the case. These couple of lines allow us to do that.
* */
const  control = this.controlDir.control;
//client side calisan validator
const validators = control.validator ? [control.validator] : [];
// Api da calisan orada birseyler kontrol eden validator.
const  asyncValidators = control.asyncValidator ? [control.asyncValidator] : [];
// the control we passed from login form  going to pass across as validators to this input and set them at the same time
control.setValidators(validators);
control.setAsyncValidators(asyncValidators);
control.updateValueAndValidity();// this is going to try and validate our form on initialization
  }

  onChange(event){}
  onTouched(){}

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  // this gives our control , value access to the values that are inputs into our input field
  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '' ;
  }

}
/*
ControlValueAccessor defines Interface that acts as bridge between the Angular forms API
and a native element in the DOM (native element = input field )

  @Self  this is for angular dependency injection it's only going to use this inside itself and not look for any

* */
