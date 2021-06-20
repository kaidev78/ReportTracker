import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationExtras  } from '@angular/router';
import { ProductRetrieve } from 'src/app/models/ProductRetrieve';
import { WebApiService } from '../../services/web-api.service';
@Component({
  selector: 'app-search-page',
  templateUrl: './search-page.component.html',
  styleUrls: ['./search-page.component.css']
})
export class SearchPageComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private webApiService: WebApiService,
              private router: Router) { }

  products: any = [];

  ngOnInit(): void {
    var name =  this.activatedRoute.queryParams.subscribe(
      params => {
        console.log(params.usersearch);
        this.webApiService.getSearchResults(params.usersearch).subscribe(
          (resp) => {
            console.log(resp.Value);
            this.products = resp.Value;
          },
          (error) => {
            console.log(error);
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
