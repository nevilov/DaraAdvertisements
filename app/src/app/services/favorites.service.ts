import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { AppComponent } from '../app.component';
import { ListOfItems, Advertisement } from '../Dtos/advertisement';
import { queryPaginated } from './queryPaginated';

@Injectable({
  providedIn: 'root',
})
export class FavoritesService {
  constructor(private http: HttpClient, private cookieService: CookieService) {}

  public getFavorites(
    queryParams?: any
  ): Observable<ListOfItems<Advertisement>> {
    let url = AppComponent.backendAddress + '/api/Favorite/get';
    let headers = new HttpHeaders({
      Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
    });
    return queryPaginated<Advertisement>(this.http, url, queryParams, headers);
  }

  public addToFavorites(advertisementId: number) {
    let url = AppComponent.backendAddress + '/api/Favorite/add';

    let options = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      }),
      params: new HttpParams().set(
        'advertisementId',
        advertisementId.toString()
      ),
    };

    return this.http.post(url, '', options);
  }

  public deleteFromFavorites(advertisementId: number) {
    let url = `${AppComponent.backendAddress}/api/Favorite/remove/${advertisementId}`;
    let options = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      }),
    };
    return this.http.delete(url, options);
  }
}
