import { ListOfItems, NewAdvertisement } from './../Dtos/advertisement';
import { AppComponent } from './../app.component';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Advertisement } from '../Dtos/advertisement';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AdvertisementService {
  constructor(private http: HttpClient, private cookieService: CookieService) {}

  public getAllAdvertisements(
    limit: number,
    offset: number
  ): Observable<ListOfItems<Advertisement>> {
    let url = AppComponent.backendAddress + '/api/Advertisement';
    let params = new HttpParams();

    if (limit) params = params.append('limit', limit.toString());
    if (offset) params = params.append('offset', offset.toString());

    return this.http.get<ListOfItems<Advertisement>>(url, { params });
  }

  public getSameAdvertisementsWithLimit(
    categoryId: number,
    limit: number
  ): Observable<ListOfItems<Advertisement>> {
    return this.http.get<ListOfItems<Advertisement>>(
      AppComponent.backendAddress +
        '/api/Advertisement?CategoryId=' +
        categoryId +
        '&Limit=' +
        limit +
        '&Offset=0&OrderBy=Id'
    );
  }

  public getAdvertisementById(id: number): Observable<Advertisement> {
    return this.http.get<Advertisement>(
      AppComponent.backendAddress + '/api/Advertisement/' + id
    );
  }

  public getAdvertisementsByCategoryId(
    id: number
  ): Observable<ListOfItems<Advertisement>> {
    return this.http.get<ListOfItems<Advertisement>>(
      AppComponent.backendAddress +
        '/api/Advertisement/category?CategoryId=' +
        id +
        '&Limit=100&Offset=0&OrderBy=Id'
    );
  }

  public createAdvertisement(advertisement: NewAdvertisement) {
    console.log('service called');

    return this.http
      .post(AppComponent.backendAddress + '/api/Advertisement', advertisement, {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      })
      .pipe(
        tap((response: any) => {
          this.cookieService.delete('LatestRedirectId');
          this.cookieService.set('LatestRedirectId', response.redirectId);
        })
      );
  }

  public updateAdvertisement(advertisement: NewAdvertisement, id: number) {
    console.log('Update service called');

    return this.http.put(
      AppComponent.backendAddress + '/api/Advertisement/' + id,
      advertisement,
      {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      }
    );
  }

  public deleteAdvertisement(id: number) {
    return this.http
      .delete(AppComponent.backendAddress + '/api/Advertisement/' + id, {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      })
      .pipe(catchError(this.checkError));
  }

  public checkError(error: any) {
    alert('Произошла ошибка: ' + error.error.error);
    console.log(error);
    return error;
  }
}
