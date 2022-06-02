import { Component, OnInit } from '@angular/core';
import {LoginComponent} from "../login/login.component";
import {AsyncValidator, AsyncValidatorFn, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AccountService} from "../account.service";
import {Router} from "@angular/router";
import {of, switchMap, timer} from "rxjs";
import {map} from "rxjs/operators";




@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errors: string [];

  constructor(private fb: FormBuilder,private accountService: AccountService,private router: Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm = this.fb.group({
      // if both of these validator passed  validateEmailNotTaken method called
      displayName: [null, [Validators.required]],
      email: [null,[Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
      [this.validateEmailNotTaken()]
      ],
      password: [null, [Validators.required]]
    });
  }
onSubmit(){
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/shop')
    },error =>  {
      console.log(error);
      this.errors = error.errors;
    })
}

// this async validator is going to be making requests to our  API to check against that email exists method we have on our API
  // this validator only called if are synchronous validators have passed validation
validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      // timer adding delay
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value){
            return of(null);
          }
          return this.accountService.checkEmailExists(control.value).pipe(
            map(res => {
              return res ? {emailExists: true} : null;
            })
          );
        })
      )
    }
}
}
