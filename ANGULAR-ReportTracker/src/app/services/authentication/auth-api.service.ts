import { Injectable } from '@angular/core';
import {Observable, of, throwError} from 'rxjs';
import {HttpClient, HttpHeaders, HttpErrorResponse} from '@angular/common/http';
import { User } from '../../models/User';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {

  private authUrl = 'http://localhost:5000/user/authtest';
  constructor(private http:HttpClient) { }

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

  authtest(userName: string, userPassword: string): Observable<any> {
    console.log("authentication test");
    return this.http.post(this.authUrl, {
      "UserName": userName,
      "UserPassword": userPassword
    });
  }

}
