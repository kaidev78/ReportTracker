import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import jwt_decode from "jwt-decode";

@Component({
  selector: 'app-customer-main-page',
  templateUrl: './customer-main-page.component.html',
  styleUrls: ['./customer-main-page.component.css']
})
export class CustomerMainPageComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
    var token = localStorage.getItem("token");
    if(token == null){
      this.router.navigate(['']);
    }
  }

  UserSearch: string = "";

  search(event:any){
    // this.router.navigate
    this.router.navigate(['/search'], {queryParams: {usersearch: this.UserSearch}})
  }
  
}
