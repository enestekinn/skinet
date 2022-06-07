import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {BehaviorSubject, of, ReplaySubject} from "rxjs";
import {IUser} from "../shared/models/user";
import {map} from "rxjs/operators";
import {Router} from "@angular/router";
import {IAddress} from "../shared/models/address";

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  // job of BehaviorSubject os to omit  immediately initial value
  // giris yapmis kullaniciyi atiyor o yuzde altaki method ile degistirdik
  //private  currentUserSource = new BehaviorSubject<IUser>(null); sayfa yeniledigimizde
  private  currentUserSource = new ReplaySubject<IUser>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient,private router: Router) { }

  // we don't need it anymore
  // getCurrentUserValue(){
  //   return this.currentUserSource.value;
  // }

  // log olduktan sonra sayfayi yeniledigimizde sag user user ismi gidiyor.
  // tokeni postmande bearer olarak gonderdik
  loadCurrentUser(token: string){

    if (token === null){
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${token}`);
    return this.http.get(this.baseUrl+`account`, {headers}).pipe(
      map((user: IUser) => {
        // we cannot get only token back from API
      localStorage.setItem('token',user.token);
      this.currentUserSource.next(user);
    }
    ));
  }

  login(values: any){
    return this.http.post(this.baseUrl + 'account/login',values).pipe(
      map( (user: IUser) => {
        if (user){
          localStorage.setItem('token',user.token)
          this.currentUserSource.next(user);
        }
    }
    ));
  }
  register(values: any){
    return this.http.post(this.baseUrl + 'account/register',values).pipe(
      map((user: IUser) => {
        if (user){
          localStorage.setItem('token',user.token)
          this.currentUserSource.next(user);

        }
      })
    );
  }
  logout(){
localStorage.removeItem('token')
    this.currentUserSource.next(null)
    this.router.navigateByUrl('/')
  }

  checkEmailExists(email: string){
    return this.http.get(this.baseUrl + 'account/emailexists?email='+email)
  }
  getUserAddress(){
    return this.http.get<IAddress>(this.baseUrl+'account/address');
  }
  updateUserAddress(address: IAddress){
    return this.http.put<IAddress>(this.baseUrl+ 'account/address',address);
  }
}


/*
we are going to want to persist in local storage si that when a user closes the browser and then comes
to our app we can check to see if they have a token and then we can automatically go and retrieve their user info from api
and log them in  to our app
* */
