import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppComponent } from '../app.component';
import { Abuse, NewAbuse, ListOfItems } from "../Dtos/abuse";
import { CookieService } from 'ngx-cookie-service';
import { catchError } from "rxjs/operators";


@Injectable({
    providedIn: 'root',
})

export class AbuseService {
    constructor(
        private http: HttpClient,
        private cookieService: CookieService) {
    }

    public getAbuses() {
        return this.http.get<ListOfItems<Abuse>>(AppComponent.backendAddress + '/api/Abuse?limit=10&offset=0', {
            headers: new HttpHeaders({
                Authorization: 'Bearer ' + this.cookieService.get('AuthToken')
            })
        });
    }

    public createAbuse(abuse: NewAbuse) {
        return this.http.post(AppComponent.backendAddress + '/api/Abuse', abuse, {
            headers: new HttpHeaders({
                Authorization: 'Bearer ' + this.cookieService.get('AuthToken')
            })
        })
        .pipe(catchError(this.checkError));
    }

    public checkError(error: any) {
        alert('Произошла ошибка: ' + error.error.error);
        console.log(error);
        return error;
    }
}