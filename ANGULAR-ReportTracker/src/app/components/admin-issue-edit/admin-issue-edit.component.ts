import { Component, OnInit } from '@angular/core';
import { IssueRetrieve } from '../../models/IssueRetrieve';
import { WebApiService } from '../../services/web-api.service';
import { IssueStatus,IssueType } from '../../../enum/IssueEnum';
import { ActivatedRoute, Router } from '@angular/router'
import { IssueStatus as IssueStatusCode } from '../../models/IssueStatus'
import { Authenticate } from '../../authenticate/authenticate'

@Component({
  selector: 'app-admin-issue-edit',
  templateUrl: './admin-issue-edit.component.html',
  styleUrls: ['./admin-issue-edit.component.css']
})
export class AdminIssueEditComponent implements OnInit {
  issue: any;
  StatusOption: any = -1;
  constructor(private route: ActivatedRoute, private router: Router,private webApiService: WebApiService,
              private authenticate: Authenticate) { }

  ngOnInit(): void {
    if(this.authenticate.admin_authenticate() == false){
      return;
    }
    var issueId:any = this.route.snapshot.paramMap.get("issueId");
    console.log("issue id is " + issueId);
    this.webApiService.getProductIssue(issueId).then(
      (service) => {
        service.subscribe(
          (resp)=>{
            this.issue = resp.Value[0];
            this.StatusOption = this.issue?.IssueStatus
            console.log(resp);
          },
          (error)=>{
            if(error.status == 401){
              this.authenticate.unauthorized_access();
            }
            console.log(error);
          }
        )
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
    var issueStatus: IssueStatusCode = {
      IssueId: this.issue?.IssueId,
      IssueStatusCode: this.StatusOption
    }
    this.webApiService.updateIssueStatus(issueStatus).then(
      (service) => {
        service.subscribe(
          (resp) => {
            console.log(resp);
            this.issue['IssueStatus'] = this.StatusOption;
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

}
