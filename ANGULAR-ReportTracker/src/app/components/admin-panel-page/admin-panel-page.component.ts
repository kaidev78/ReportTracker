import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import jwt_decode from "jwt-decode";
import { AccountType } from "../../../enum/AccountTypeEnum";

@Component({
  selector: 'app-admin-panel-page',
  templateUrl: './admin-panel-page.component.html',
  styleUrls: ['./admin-panel-page.component.css']
})
export class AdminPanelPageComponent implements OnInit {

  current_url: string = "";
  getRouter: Router = this.router;

  constructor(private router: Router) { 
  }

  ngOnInit(): void {
    var token:any = localStorage.getItem("token");
    if(token == null){
      this.router.navigate(['']);
    }
    var decoded:any = jwt_decode(token);
    console.log(decoded["AccountType"]);
    console.log(AccountType.ADMIN);
    if(decoded["AccountType"] != AccountType.ADMIN){
      this.router.navigate(['/home']);
    }
    this.current_url = this.router.url;
  }


  changeMenu(event:any){
    // this.current_url = this.router.url;
  }
}
