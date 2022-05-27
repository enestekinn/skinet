import { Component, OnInit } from '@angular/core';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

// Buradan deger veriyoruz bu degeri  html den cekiyoruz.
export class AppComponent  implements OnInit{
  
  title = 'Skinet';
  
 

  constructor(){}

  ngOnInit(): void {

  }
}
