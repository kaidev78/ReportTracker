import { Component, OnInit } from '@angular/core';
import { WebApiService } from '../../services/web-api.service';
import { User } from '../../models/User';
import { Router } from '@angular/router';
@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit {

  constructor(private webService: WebApiService, private router: Router) {}

  UserOption: Number = 1;
  UserEmail: string = "";
  UserPassword: string = "";
  UserConfirmPassword: string = "";
  UserName: string = "";
  UserExist: boolean = false;

  ngOnInit(): void {
  }

  register(event: any){
    console.log("option: " + this.UserOption);
    console.log("email " + this.UserEmail);
    console.log("password " + this.UserPassword);
    console.log("confirm password " + this.UserConfirmPassword);
    // Simple password check
    if(this.UserPassword != this.UserConfirmPassword){
      console.log("password doesn't match");
    }
    else{
      console.log("register request is sent");
      var user: User = {
        UserEmail: this.UserEmail,
        UserName: this.UserName,
        UserPassword: this.UserPassword,
        UserType: this.UserOption
      };

      this.webService.register(user).subscribe(
        (resp)=>{
          console.log("response: " + resp);
          console.log("sucessfull registered");
          this.router.navigate(['login']);

        },
        (error)=>{
          console.log("error " + error.status)
          if(error.status == 400){
            this.UserExist = true;
          }
        }
      );
    }
  }

  // Go back to login page
  back(event: any){
    this.router.navigate(['']);
  }

}
