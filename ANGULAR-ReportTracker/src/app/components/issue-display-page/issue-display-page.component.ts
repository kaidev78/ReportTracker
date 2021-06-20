import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IssueRetrieve } from '../../models/IssueRetrieve';
import { WebApiService } from '../../services/web-api.service';
import { IssueStatus,IssueType } from '../../../enum/IssueEnum';

@Component({
  selector: 'app-issue-display-page',
  templateUrl: './issue-display-page.component.html',
  styleUrls: ['./issue-display-page.component.css']
})
export class IssueDisplayPageComponent implements OnInit {

  issue?: IssueRetrieve;
  constructor(private route: ActivatedRoute, private webApiService: WebApiService) { }

  ngOnInit(): void {
    var productId:any = this.route.snapshot.queryParamMap.get("issueId");
    this.webApiService.getProductIssue(productId).subscribe(
      (resp)=>{
        this.issue = resp.Value[0];
        console.log(this.issue);
      },
      (error)=>{
        console.log(error);
      }
    )
  }

  getIssueStatus(status: number){
    return IssueStatus[status];
  }

  getIssueType(type: number){
    return IssueType[type];
  }

}
