import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationExtras  } from '@angular/router';
import { ProductRetrieve } from 'src/app/models/ProductRetrieve';
import { WebApiService } from '../../services/web-api.service';
import { Authenticate } from '../../authenticate/authenticate';

@Component({
  selector: 'app-search-page',
  templateUrl: './search-page.component.html',
  styleUrls: ['./search-page.component.css']
})
export class SearchPageComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private webApiService: WebApiService,
              private router: Router, private authenticate: Authenticate) { }

  products: any = [];

  ngOnInit(): void {
    if(this.authenticate.normal_authenticate() == false){
      return;
    }
    var name =  this.activatedRoute.queryParams.subscribe(
      params => {
        console.log(params.usersearch);
        this.webApiService.getSearchResults(params.usersearch).then(
          (service) => {
            service.subscribe(
              (resp) => {
                console.log(resp.Value);
                this.products = resp.Value;
              },
              (error) => {
                if(error.status == 401){
                  this.authenticate.unauthorized_access();
                }
                console.log(error);
              }
            )
          }
        )
      }
    )
    console.log(name);
  }

  goIssuePage(product: ProductRetrieve){
    console.log(product.ProductName);
    console.log(product.ProductId);
    this.router.navigate(['/issues'], {queryParams: {productId: product.ProductId}})
  }

}
