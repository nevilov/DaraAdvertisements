import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {AppComponent} from "../app.component";
import {CookieService} from "ngx-cookie-service";

@Injectable({
  providedIn: 'root'
})
export class ChatService{
  constructor(private http: HttpClient, private cookieService: CookieService) {

  }

  public getChat(advertisementId: number){
    return this.http.get(AppComponent.backendAddress + `/api/chat/get/${advertisementId}`, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      }),
    });
  }

  public save(advertisementId: number, text: string, customerId?: string){
    return this.http.post(AppComponent.backendAddress + '/api/chat/save', {advertisementId, text, customerId}), {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      }),
    };
  }
}
