import { Component, OnInit } from '@angular/core';
import {  WebApiService } from '../../services/web-api.service';
import { Product } from '../../models/Product';
@Component({
  selector: 'app-admin-project-list-panel',
  templateUrl: './admin-project-list-panel.component.html',
  styleUrls: ['./admin-project-list-panel.component.css']
})
export class AdminProjectListPanelComponent implements OnInit {

  constructor(private webApiService: WebApiService) { }

  products = [];  
  ngOnInit(): void {
    this.webApiService.getProducts().then(
      (service) => {
        service.subscribe(
          (resp)=>{
            console.log(resp.Value);
            this.products = resp.Value;
          },
          (error)=>{
            console.log(error);
          }
        );
      }
    )
  }

}
