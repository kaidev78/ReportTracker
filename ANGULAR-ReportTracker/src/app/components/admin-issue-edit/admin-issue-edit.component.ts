import { Component, OnInit } from '@angular/core';
import { IssueRetrieve } from '../../models/IssueRetrieve';
import { WebApiService } from '../../services/web-api.service';
import { IssueStatus,IssueType } from '../../../enum/IssueEnum';
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-admin-issue-edit',
  templateUrl: './admin-issue-edit.component.html',
  styleUrls: ['./admin-issue-edit.component.css']
})
export class AdminIssueEditComponent implements OnInit {
  issue?: IssueRetrieve;
  StatusOption?: number = -1;
  constructor(private route: ActivatedRoute, private webApiService: WebApiService) { }

  ngOnInit(): void {
    var issueId:any = this.route.snapshot.paramMap.get("issueId");
    console.log("issue id is " + issueId);
    this.webApiService.getProductIssue(issueId).subscribe(
      (resp)=>{
        this.issue = resp.Value[0];
        this.StatusOption = this.issue?.IssueStatus
        console.log(resp);
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

  updateStatus(){
    console.log("status will be updated " + this.StatusOption);
  }

}
