import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import jwt_decode from "jwt-decode";
import { AccountType } from "../../enum/AccountTypeEnum";

@Injectable({
    providedIn: 'root'
})

export class Authenticate{

    constructor(private router:Router) { }
    
    admin_authenticate():boolean{
        var token:any = localStorage.getItem("JwtToken");
        if(token == null){
          this.router.navigate(['']);
          return false;
        }
        var decoded:any = jwt_decode(token);
        console.log(decoded["AccountType"]);
        console.log(AccountType.ADMIN);
        if(decoded["AccountType"] != AccountType.ADMIN){
          this.router.navigate(['/home']);
          return false;
        }
        return true;
    }

    normal_authenticate():boolean{
        var token:any = localStorage.getItem("JwtToken");
        if(token == null){
            this.router.navigate(['']);
            return false
        }
        return true
    }

}