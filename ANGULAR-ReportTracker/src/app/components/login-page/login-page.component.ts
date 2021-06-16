import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthApiService } from '../../services/authentication/auth-api.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  UserName: string = "test";
  UserPassword: string = "password";

  constructor(private router:Router, private auth: AuthApiService) { }

  ngOnInit(): void {
    console.log("init: " + localStorage.getItem('token'));
    
  }

  /*Login user with provided username and password */
  login(event: any){
    console.log("username: " + this.UserName)
    console.log("password: " + this.UserPassword)
    this.auth.authtest(this.UserName, this.UserPassword).subscribe(
      (resp)=>{
        var response = JSON.stringify(resp)
        console.log("string: " + response);
        console.log("token: " + resp['Value']['token']);
        console.log("type: " + resp['Value']['type']);
        localStorage.setItem('token', resp['Value']['token']);
        var type = resp['Value']['type'];
        // 1 for developer
        if(type == 1){
          
        }
        else if(type == 2){

        }
        // 2 for customer
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
