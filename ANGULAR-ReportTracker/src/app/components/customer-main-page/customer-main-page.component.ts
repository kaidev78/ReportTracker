import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customer-main-page',
  templateUrl: './customer-main-page.component.html',
  styleUrls: ['./customer-main-page.component.css']
})
export class CustomerMainPageComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  UserSearch: string = "";

  search(event:any){
    // this.router.navigate
    this.router.navigate(['/search'], {queryParams: {usersearch: this.UserSearch}})
  }
  
}
