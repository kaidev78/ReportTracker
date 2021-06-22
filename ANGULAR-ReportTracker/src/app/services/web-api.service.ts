import { Injectable } from '@angular/core';
import {Observable, of, throwError} from 'rxjs';
import {HttpClient, HttpHeaders, HttpErrorResponse} from '@angular/common/http';
import { User } from '../models/User';
import { Product } from '../models/Product';
import { catchError } from 'rxjs/operators';
import { Issue } from '../models/Issue';
import { IssueStatus } from '../models/IssueStatus';
import jwt_decode from "jwt-decode";
import { RefreshCred } from '../models/RefreshCred';

@Injectable({
  providedIn: 'root'
})
export class WebApiService {

  private registerUrl = 'http://localhost:5001/user/register';
  private addProductUrl = 'http://localhost:5002/api/ProductPublisher';
  private getProductListUrl = 'http://localhost:5000/product-list';
  private getSearchResultsUrl = 'http://localhost:5000/search/';
  private getProductUrl = 'http://localhost:5000/api/Product/';
  private sendIssueUrl = 'http://localhost:5002/send-issue';
  private getProductIssuesUrl = 'http://localhost:5000/product-issues/';
  private getProductIssueUrl = 'http://localhost:5000/product-issue/';
  private deleteIssueUrl = 'http://localhost:5000/delete-issue/';
  private updateIssueStatusUrl  = 'http://localhost:5000/update-issue-status';
  private refreshTokenUrl = 'http://localhost:5001/api/user/refresh';

  constructor(private http:HttpClient) { }
  token: any = localStorage.getItem("JwtToken");
  headers = new HttpHeaders()
  .set("Authorization", "Bearer " + this.token);
  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      {
        messsage: error.error,
        status: error.status
      });
  }

  async checkTokenExpireTime(){
    var decoded:any = jwt_decode(this.token);
    var currentTime = new Date().getTime() / 1000;
    console.log(currentTime);
    console.log(parseInt(decoded['exp']));
    if(currentTime> parseInt(decoded['exp'])){
      console.log("call refresh to refresh token");
      var refreshCred: RefreshCred = {
        JwtToken: localStorage.getItem("JwtToken"),
        RefreshToken: localStorage.getItem("RefreshToken")
      }
      await this.http.post<any>(this.refreshTokenUrl, refreshCred).toPromise().then(
        (resp) => {
          console.log(resp);
          localStorage.setItem('JwtToken', resp['JwtToken']);
          localStorage.setItem('RefreshToken', resp['RefreshToken']);
        },
        (error) => {
          console.log(error);
        }
      )
      
    }
    else{
      console.log("token is not expired yet");
    }
  }


  register(user: User): Observable<any> {
    console.log("register is called");
    return this.http.post(this.registerUrl, user).pipe(
      catchError(this.handleError)
    );
  }

  async addProduct(product: Product): Promise<Observable<any>>{
    console.log("add product is called");
    await this.checkTokenExpireTime();
    return this.http.post(this.addProductUrl, product, {"headers": this.headers}).pipe(
      catchError(this.handleError)
    )
  }

  async getProducts(): Promise<Observable<any>>{
    console.log("get product is called");
    await this.checkTokenExpireTime();
    return this.http.get(this.getProductListUrl, {"headers": this.headers}).pipe(
      catchError(this.handleError)
    );
  }

  async getSearchResults(productName: string): Promise<Observable<any>>{
    await this.checkTokenExpireTime();
    return this.http.get(this.getSearchResultsUrl+productName, {"headers": this.headers}).pipe(
      catchError(this.handleError)
    );
  }

  async getProduct(productId: number): Promise<Observable<any>>{
    await this.checkTokenExpireTime();
    return this.http.get(this.getProductUrl+productId, {"headers": this.headers}).pipe(
      catchError(this.handleError)
    );
  }

  async sendIssue(issue: Issue): Promise<Observable<any>>{
    await this.checkTokenExpireTime();
    return this.http.post(this.sendIssueUrl, issue, {"headers": this.headers}).pipe(
      catchError(this.handleError)
    );
  }

  async getProductIssues(productId: number): Promise<Observable<any>>{
    await this.checkTokenExpireTime();
    return this.http.get(this.getProductIssuesUrl+productId, {"headers": this.headers});
  }

  getProductIssuesWithoutRefresh(productId: number): Observable<any>{
    return this.http.get(this.getProductIssuesUrl+productId, {"headers": this.headers});
  }

  async getProductIssue(issueId: number): Promise<Observable<any>>{
    await this.checkTokenExpireTime();
    return this.http.get(this.getProductIssueUrl+issueId, {"headers": this.headers});
  }

  async deleteProductIssue(issueId: number): Promise<Observable<any>>{
    await this.checkTokenExpireTime();
    return this.http.delete(this.deleteIssueUrl+issueId, {"headers": this.headers});
  }

  async updateIssueStatus(issueStatus: IssueStatus): Promise<Observable<any>>{
    await this.checkTokenExpireTime();
    return this.http.post(this.updateIssueStatusUrl, issueStatus, {"headers": this.headers});
  }

}
