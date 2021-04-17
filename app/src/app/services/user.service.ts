import { User } from './../Dtos/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { AppComponent } from '../app.component';
import { Advertisement, ListOfItems } from '../Dtos/advertisement';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient, private cookieService: CookieService) {}
  public getUserAdvertisements(
    id: string
  ): Observable<ListOfItems<Advertisement>> {
    // alert(id);
    return this.http.get<ListOfItems<Advertisement>>(
      AppComponent.backendAddress + '/api/Advertisement/get/useradv?Id=' + id
    );
  }
  public getUser(name: string): Observable<User> {
    return this.http.get<User>(
      AppComponent.backendAddress + '/api/User/get/' + name
    );
  }
}
