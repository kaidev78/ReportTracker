import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { WebApiService } from '../../services/web-api.service';
import { IssueStatus,IssueType } from '../../../enum/IssueEnum';
import { IssueRetrieve } from '../../models/IssueRetrieve'
import { Authenticate } from '../../authenticate/authenticate'

@Component({
  selector: 'app-issue-admin-panel',
  templateUrl: './issue-admin-panel.component.html',
  styleUrls: ['./issue-admin-panel.component.css']
})
export class IssueAdminPanelComponent implements OnInit {

  constructor(private router: Router, private activatedRoute: ActivatedRoute, 
    private webApiService: WebApiService, private authenticate: Authenticate) { }
  
  issues: IssueRetrieve[] = [];

  ngOnInit(): void {
    if(this.authenticate.admin_authenticate()==false){
      return;
    }
    var productId:any = this.activatedRoute.snapshot.paramMap.get('productId');
    console.log(productId);
    this.webApiService.getProductIssues(productId).then(
      (service) => {
        service.subscribe(
          (resp) => {
            console.log(resp)
            this.issues = resp.Value;
          },
          (error) => {
            if(error.status == 401){
              this.authenticate.unauthorized_access();
            }
            console.log(error);
          }
        );
      }
    )
  }
  goIssueDisplayPage(issueId: number){
    console.log("issue id is " + issueId);
  }

  getIssueStatus(status: number){
    return IssueStatus[status];
  }

  getIssueType(type: number){
    return IssueType[type];
  }

  deleteIssue(issueId: number, index: number){
    console.log("delete " + issueId);
    console.log("index " + index);
    this.webApiService.deleteProductIssue(issueId).then(
      (service) => {
        service.subscribe(
          (resp)=>{
            this.issues.splice(index, 1);
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

}
