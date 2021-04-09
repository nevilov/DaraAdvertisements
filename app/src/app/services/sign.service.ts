import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppComponent } from '../app.component';
import { CookieService } from 'ngx-cookie-service';
import { catchError, tap } from 'rxjs/operators';
import { DataSharingService } from './datasharing.service';
import { resetPassword } from './../Dtos/resetPassword';


@Injectable({
    providedIn: 'root'
})

export class SignService {

    constructor(
        private http: HttpClient,
        private cookieService: CookieService,
        private dataSharingService: DataSharingService) {}

    public register(user: any): Observable<any> {
        return this.http
        .post(AppComponent.backendAddress + '/api/User/register', user)
        .pipe(catchError(this.checkError));
    }

    public login(user: any): Observable<any> {
        return this.http
        .post(AppComponent.backendAddress + '/api/User/login', user)
        .pipe(
            tap((response: any) => {
                        this.cookieService.set('AuthUsername', user.login);
                        this.cookieService.set('AuthToken', response.token);
                        this.dataSharingService.isUserLoggedIn.next(true);
            }),
            catchError(this.checkError));
    }

    public checkError(error: any) {
        alert('Произошла ошибка: ' + error.error.error);
        console.log(error);
        return error;
    }

    public logout() {
        this.cookieService.deleteAll();
        this.dataSharingService.isUserLoggedIn.next(true);
    }

    public forgotPassword(email: string): Observable<any>{
      return this.http
        .get(AppComponent.backendAddress + '/api/User/forgotPassword/' + email);
      console.log('Отправлено на почту ' + email);
    }

    public resetPassword(resetPasswordRequest: resetPassword): Observable<any>{
      return this.http
        .post(AppComponent.backendAddress + '/api/user/resetPassword', resetPasswordRequest).pipe(
          tap((response: any) => {
            this.cookieService.set('LatestRedirectId', '/');
          }),
          catchError(this.checkError)
        );
    }
}
