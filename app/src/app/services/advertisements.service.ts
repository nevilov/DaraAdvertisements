import { ListOfItems, NewAdvertisement } from './../Dtos/advertisement';
import { AppComponent } from './../app.component';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Advertisement } from '../Dtos/advertisement';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})
export class AdvertisementService {
    // public adv$: BehaviorSubject<Advertisement[]>

    constructor(private http: HttpClient, private cookieService: CookieService) {
        // this.adv$ = new BehaviorSubject(null);
    }

    public getAllAdvertisements(): Observable<ListOfItems<Advertisement>> {
        return this.http.get<ListOfItems<Advertisement>>(
            AppComponent.backendAddress + '/api/Advertisement?limit=100&offset=0'
        );
    }

    public getSameAdvertisementsWithLimit(categoryId: number, limit: number): Observable<ListOfItems<Advertisement>> {
        return this.http.get<ListOfItems<Advertisement>>(
            AppComponent.backendAddress + '/api/Advertisement?CategoryId=' + categoryId + '&Limit=' + limit + '&Offset=0&OrderBy=Id'
        );
    }

    public getAdvertisementById(id: number): Observable<Advertisement> {
        return this.http.get<Advertisement>(
            AppComponent.backendAddress + '/api/Advertisement/' + id
        );
    }

    public getAdvertisementByCategoryId(id: number): Observable<ListOfItems<Advertisement>> {
        return this.http.get<ListOfItems<Advertisement>>(
            AppComponent.backendAddress + '/api/Advertisement/category?CategoryId=' + id + '&Limit=10000&Offset=0'
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
                    this.cookieService.set('LatestRedirectId', response.redirectId);
                }),
                catchError(this.checkError)
            );
    }

    public checkError(error: any) {
        alert('Произошла ошибка: ' + error.error.error);
        console.log(error);
        return error;
    }
}
