import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppComponent } from '../app.component';
import { catchError, tap } from 'rxjs/operators';
import { DataSharingService } from './datasharing.service';
import { ToastrService } from 'ngx-toastr';
import { resetPassword } from './../Dtos/resetPassword';

export interface SignInResult {
  token: string;
}

@Injectable({
  providedIn: 'root',
})
export class SignService {
  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private dataSharingService: DataSharingService,
    private router: Router,
    public toastr: ToastrService
  ) {}

  public register(user: any): Observable<any> {
    return this.http.post(
      AppComponent.backendAddress + '/api/User/register',
      user
    );
  }

  public login(user: any): Observable<SignInResult> {
    return this.http
      .post<SignInResult>(AppComponent.backendAddress + '/api/User/login', user)
      .pipe(
        tap((response: any) => {
          this.cookieService.set('AuthUsername', response.userName, 1, '/');
          this.cookieService.set('AuthToken', response.token, 1, '/');
          this.cookieService.set('UserAvatar', response.userAvatar, 1, '/');
          this.cookieService.set('UserRole', response.userRole, 1, '/');
          this.cookieService.set('UserId', response.userId, 1, '/');
          this.dataSharingService.isUserLoggedIn.next(true);
        })
      );
  }

  public logout() {
    this.cookieService.deleteAll();
    this.cookieService.deleteAll('/');
    this.dataSharingService.isUserLoggedIn.next(true);
    this.router.navigateByUrl('/');
  }

  public forgotPassword(email: string): Observable<any> {
    return this.http
      .get(AppComponent.backendAddress + '/api/User/forgotPassword/' + email)
      .pipe(
        tap((response: any) => {
          this.cookieService.set('LatestRedirectId', '/');
        })
      );
  }

  public resetPassword(resetPasswordRequest: resetPassword): Observable<any> {
    return this.http
      .post(
        AppComponent.backendAddress + '/api/user/resetPassword',
        resetPasswordRequest
      )
      .pipe(
        tap((response: any) => {
          this.cookieService.set('redirect', '/');
        })
      );
  }
}
