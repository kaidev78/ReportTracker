import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

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
    this.current_url = this.router.url;
  }


  changeMenu(event:any){
    // this.current_url = this.router.url;
  }
}
