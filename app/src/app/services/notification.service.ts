import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {AppComponent} from "../app.component";
import {CookieService} from "ngx-cookie-service";

@Injectable({
  providedIn: 'root'
})

export class NotificationService{
  constructor(private http: HttpClient, private cookieService: CookieService) {
  }

  public send(subject: string, message: string){
    console.log(subject  +  message);
    return this.http.post(AppComponent.backendAddress + '/api/Notification/send', {subject, message}, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      })
    });
  }

}
