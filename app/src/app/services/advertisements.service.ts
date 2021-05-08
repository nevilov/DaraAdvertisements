import { ListOfItems, NewAdvertisement } from './../Dtos/advertisement';
import { AppComponent } from './../app.component';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Advertisement } from '../Dtos/advertisement';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class AdvertisementService {
  constructor(private http: HttpClient, private cookieService: CookieService) {}

  public getAllAdvertisements(
    queryParams?: any
  ): Observable<ListOfItems<Advertisement>> {
    let url = AppComponent.backendAddress + '/api/Advertisement';
    return this.queryPaginated<Advertisement>(this.http, url, queryParams);
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

  public importExcel(excelFile: File) {
    const formExcel = new FormData();
    formExcel.append('excel', excelFile, excelFile.name);
    return this.http.post(
      AppComponent.backendAddress + '/api/Advertisement/import',
      formExcel,
      {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
        }),
      }
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

  private queryPaginated<T>(
    http: HttpClient,
    baseUrl: string,
    queryParams?: any
  ): Observable<ListOfItems<T>> {
    let params = new HttpParams();
    let url = baseUrl;

    Object.keys(queryParams)
      .sort()
      .forEach((key) => {
        const value = queryParams[key];
        if (value !== null && value !== '') {
          params = params.set(key, value.toString());
        }
      });

    return http.get<ListOfItems<T>>(url, {
      params,
    });
  }

  public checkError(error: any) {
    alert('Произошла ошибка: ' + error.error.error);
    console.log(error);
    return error;
  }
}
