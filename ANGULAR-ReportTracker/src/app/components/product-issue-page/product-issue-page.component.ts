import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationExtras } from '@angular/router';
import { IssueRetrieve } from 'src/app/models/IssueRetrieve';
import { ProductRetrieve } from 'src/app/models/ProductRetrieve';
import { WebApiService } from '../../services/web-api.service';
import { IssueStatus,IssueType } from '../../../enum/IssueEnum';
import { Authenticate } from '../../authenticate/authenticate';

@Component({
  selector: 'app-product-issue-page',
  templateUrl: './product-issue-page.component.html',
  styleUrls: ['./product-issue-page.component.css']
})
export class ProductIssuePageComponent implements OnInit {

  products: any = [];
  issues: IssueRetrieve[] = [];
  constructor(private router: Router, private webApiService: WebApiService,
              private activatedRoute: ActivatedRoute, private authenticate: Authenticate) {
   }

  ngOnInit(): void {
    if(this.authenticate.normal_authenticate() == false){
      return;
    }
    this.activatedRoute.queryParams.subscribe(
      params => {
        this.webApiService.getProduct(params.productId).subscribe(
          (resp) => {
            this.products = resp.Value;
            var product: ProductRetrieve = resp.Value[0];
            console.log(this.products);
            this.webApiService.getProductIssues(product.ProductId).subscribe(
              (resp)=>{
                console.log(resp.Value);
                this.issues = resp.Value;
              },
              (error)=>{
                console.log(error);
              }
            )
          },
          (error) => {
            console.log(error);
          }
        )
      }
    )
  }

  goIssueForm(){
    var product: ProductRetrieve = this.products[0]
    this.router.navigate(['/create-issue'], {queryParams: {productId: product.ProductId, productName: product.ProductName}})
  }

  getIssueStatus(status: number){
    return IssueStatus[status];
  }

  getIssueType(type: number){
    return IssueType[type];
  }

  goIssueDisplayPage(issueId: number){
    this.router.navigate(['/issue-display'], {queryParams: {issueId: issueId}})
  }

}
