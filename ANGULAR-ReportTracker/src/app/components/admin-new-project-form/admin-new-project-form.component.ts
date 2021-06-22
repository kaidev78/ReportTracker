import { Component, OnInit } from '@angular/core';
import { WebApiService } from '../../services/web-api.service';
import { Product } from '../../models/Product';
import { Authenticate } from '../../authenticate/authenticate';

@Component({
  selector: 'app-admin-new-project-form',
  templateUrl: './admin-new-project-form.component.html',
  styleUrls: ['./admin-new-project-form.component.css']
})
export class AdminNewProjectFormComponent implements OnInit {

  constructor(private webService: WebApiService, private authenticate: Authenticate) { }

  ngOnInit(): void {
  }

  ProductName: string = "";
  ProductDescription: string = "";

  submitProductForm(){
    console.log("product name: " + this.ProductName);
    console.log("product description: " + this.ProductDescription);

    var product: Product = {
      ProductName: this.ProductName,
      ProductDescription: this.ProductDescription
    };
    this.webService.addProduct(product).then(
      (service) => {
        service.subscribe(
          (resp)=>{
            console.log("response: " + resp);
            console.log("sucessfull registered");
          },
          (error)=>{
            if(error.status == 401){
              this.authenticate.unauthorized_access();
            }
            console.log("error " + error.status)
          }
        );
      }
    )
  }

}
