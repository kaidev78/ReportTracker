import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  UserEmail: string = "";
  UserPassword: string = "";

  constructor() { }

  ngOnInit(): void {
  }

  /*Login user with provided username and password */
  login(event: any){
    console.log("email: " + this.UserEmail)
    console.log("password: " + this.UserPassword)
    
  }

}
