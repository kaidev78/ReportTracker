import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import jwt_decode from "jwt-decode";
import { Authenticate } from '../../authenticate/authenticate'

@Component({
  selector: 'app-customer-main-page',
  templateUrl: './customer-main-page.component.html',
  styleUrls: ['./customer-main-page.component.css']
})
export class CustomerMainPageComponent implements OnInit {

  constructor(private router: Router, private authenticate: Authenticate) { }

  ngOnInit(): void {
    this.authenticate.normal_authenticate();
  }

  UserSearch: string = "";

  search(event:any){
    // this.router.navigate
    this.router.navigate(['/search'], {queryParams: {usersearch: this.UserSearch}})
  }
  
}
