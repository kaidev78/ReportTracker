import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthApiService } from '../../services/authentication/auth-api.service';
import jwt_decode from "jwt-decode";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  UserName: string = "";
  UserPassword: string = "";

  constructor(private router:Router, private auth: AuthApiService) { }

  ngOnInit(): void {
  }

  /*Login user with provided username and password */
  login(event: any){
    console.log("username: " + this.UserName)
    console.log("password: " + this.UserPassword)
    this.auth.authtest(this.UserName, this.UserPassword).subscribe(
      (resp)=>{
        // var response = JSON.stringify(resp)
        console.log(resp);
        localStorage.setItem('JwtToken', resp['JwtToken']);
        localStorage.setItem('RefreshToken', resp['RefreshToken']);
        var token = resp['JwtToken'];
        var decoded = jwt_decode<any>(token);
        var type = decoded['AccountType'];
        console.log("type is " );
        // 1 for developer
        if(type == 2){
          this.router.navigate(['admin-panel']);
        }
        // 2 for customer
        else if(type == 1){
          this.router.navigate(['home']);
        }

      
      },
      (error)=>{
        console.log("error " + error.status)
        if(error.status == 400){
          
        }
      }
    );
    
  }

  // Go to register site
  goRegister(event: any){
    this.router.navigate(['register']);
  }

}
