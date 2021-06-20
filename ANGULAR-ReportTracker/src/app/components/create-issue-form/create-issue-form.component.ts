import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductRetrieve } from 'src/app/models/ProductRetrieve';
import { Issue } from 'src/app/models/Issue';
import { WebApiService } from '../../services/web-api.service';

@Component({
  selector: 'app-create-issue-form',
  templateUrl: './create-issue-form.component.html',
  styleUrls: ['./create-issue-form.component.css']
})
export class CreateIssueFormComponent implements OnInit {

  IssueType:number = 1;
  IssueName:string = "";
  IssueDescription: string = "";

  constructor(private router: Router, private route: ActivatedRoute,
              private webApiService: WebApiService) {
   }

  ngOnInit(): void {
    
  }

  submitIssueForm(){
    var productId:any = this.route.snapshot.queryParamMap.get("productId");
    console.log("Issue Type: " + this.IssueType + "\n" 
                + "Issue Name: " + this.IssueName + "\n"
                + "Issue Description: " + this.IssueDescription + "\n"
                + "Product Id: " + productId);
    var issue:Issue = {
      IssueType: this.IssueType,
      IssueName: this.IssueName,
      IssueDescription: this.IssueDescription,
      ProductId: parseInt(productId)
    }

    console.log(issue);

    this.webApiService.sendIssue(issue).subscribe(
      (resp)=>{
        console.log(resp)
      },
      (error)=>{
        console.log(error)
      }
    )    

  }



}