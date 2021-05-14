import { User } from './../Dtos/user';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { from, Observable } from 'rxjs';
import { AppComponent } from '../app.component';
import { Advertisement, ListOfItems } from '../Dtos/advertisement';

import { queryPaginated } from './queryPaginated';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  isCorporation: boolean = false;

  constructor(private http: HttpClient, private cookieService: CookieService) {
    this.getCurrentUser().subscribe((response) => {
      this.isCorporation = response.isCorporation;
    });
  }

  public getUserAdvertisements(
    queryParams?: any
  ): Observable<ListOfItems<Advertisement>> {
    let url = AppComponent.backendAddress + '/api/Advertisement/get/useradv';
    return queryPaginated<Advertisement>(this.http, url, queryParams);
  }

  public getCurrentUser(): Observable<User> {
    return this.http.get<User>(
      AppComponent.backendAddress + '/api/User/get?isCurrent=true',
      {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      }
    );
  }

  public getUserAdvertisementsWithLimit(
    id: string,
    limit: number,
    offset: number
  ): Observable<ListOfItems<Advertisement>> {
    return this.http.get<ListOfItems<Advertisement>>(
      AppComponent.backendAddress +
        '/api/Advertisement/get/useradv?Id=' +
        id +
        '&Limit=' +
        limit +
        '&Offset=' +
        offset +
        '&OrderBy=Id'
    );
  }

  public getUser(name: string): Observable<User> {
    return this.http.get<User>(
      AppComponent.backendAddress + '/api/User/get/' + name
    );
  }

  public changePassword(form: any): Observable<any> {
    return this.http.patch(
      AppComponent.backendAddress + '/api/User/ChangePassword',
      form,
      {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      }
    );
  }

  public changeEmail(email: string) {
    let url =
      AppComponent.backendAddress +
      '/api/User/send/changeEmailToken?newEmail=' +
      email;
    let header = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      }),
    };
    return this.http.get(url, header);
  }

  public updateUserInfo(form: any) {
    let url = AppComponent.backendAddress + '/api/User/update/domainuser';
    let header = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      }),
    };

    return this.http.patch(url, form, header);
  }

  public blockUser(form: any): Observable<any> {
    return this.http.post(
      AppComponent.backendAddress + '/api/User/block',
      form,
      {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      }
    );
  }

  public changeRole(form: any): Observable<any> {
    return this.http.post(
      AppComponent.backendAddress + '/api/User/changerole',
      form,
      {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      }
    );
  }

  public changeCorporationStatus(userId: string, isCorporation: boolean) {
    return this.http.patch(
      AppComponent.backendAddress + '/api/User/changeUserStatus',
      { userId, isCorporation },
      {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      }
    );
  }

  public setNotificationState(isSubscribe: boolean): Observable<any> {
    return this.http.patch(
      AppComponent.backendAddress + '/api/User/notifications',
      { isSubscribe },
      {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      }
    );
  }
}
