import { Component, OnInit } from '@angular/core';
import { WebApiService } from '../../services/web-api.service';
import { Product } from '../../models/Product';
@Component({
  selector: 'app-admin-new-project-form',
  templateUrl: './admin-new-project-form.component.html',
  styleUrls: ['./admin-new-project-form.component.css']
})
export class AdminNewProjectFormComponent implements OnInit {

  constructor(private webService: WebApiService) { }

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
    this.webService.addProduct(product).subscribe(
      (resp)=>{
        console.log("response: " + resp);
        console.log("sucessfull registered");
      },
      (error)=>{
        console.log("error " + error.status)
        if(error.status == 400){
          
        }
      }
    );
  }

}
