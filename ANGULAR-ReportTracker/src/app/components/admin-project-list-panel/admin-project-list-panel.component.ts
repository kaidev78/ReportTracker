import { Component, OnInit } from '@angular/core';
import {  WebApiService } from '../../services/web-api.service';
import { Authenticate } from '../../authenticate/authenticate';
@Component({
  selector: 'app-admin-project-list-panel',
  templateUrl: './admin-project-list-panel.component.html',
  styleUrls: ['./admin-project-list-panel.component.css']
})
export class AdminProjectListPanelComponent implements OnInit {

  constructor(private webApiService: WebApiService, private authenticate: Authenticate) { }

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
            if(error.status == 401){
              this.authenticate.unauthorized_access();
            }
            console.log(error);
          }
        );
      }
    )
  }

}
