import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppComponent } from '../app.component';
import { Abuse, NewAbuse, ListOfItems } from '../Dtos/abuse';
import { CookieService } from 'ngx-cookie-service';
import { catchError } from 'rxjs/operators';
import { queryPaginated } from './queryPaginated';

@Injectable({
  providedIn: 'root',
})
export class AbuseService {
  constructor(private http: HttpClient, private cookieService: CookieService) {}

  public getAbuses(queryParams: any) {
    let url = AppComponent.backendAddress + '/api/Abuse';
    let headers = new HttpHeaders({
      Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
    });
    return queryPaginated<Abuse>(this.http, url, queryParams, headers);
  }

  public createAbuse(abuse: NewAbuse) {
    return this.http.post(AppComponent.backendAddress + '/api/Abuse', abuse, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      }),
    });
    //   .pipe(catchError(this.checkError));
  }

  public closeAbuse(closeId: number) {
    return this.http
      .delete(AppComponent.backendAddress + '/api/Abuse/' + closeId, {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      })
      .pipe(catchError(this.checkError));
  }

  public checkError(error: any) {
    console.log(error);
    return error;
  }
}
