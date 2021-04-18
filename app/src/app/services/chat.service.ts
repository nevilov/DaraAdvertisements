import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {AppComponent} from "../app.component";
import {CookieService} from "ngx-cookie-service";
import {Observable} from "rxjs";
import {Chat, ChatResponse} from "../Dtos/chat";

@Injectable({
  providedIn: 'root'
})
export class ChatService{
  constructor(private http: HttpClient, private cookieService: CookieService) {

  }

  public getChats(isSeller: boolean): Observable<ChatResponse>{
    return this.http.get<ChatResponse>(AppComponent.backendAddress + `/api/chat/get/${isSeller}`,{
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      }),
    });
  }

  public getChat (advertisementId: number){
    return this.http.get(AppComponent.backendAddress + `/api/chat/get/${advertisementId}`, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      }),
    });
  }

  public createChat(advertisementId: number){
    return this.http.post(AppComponent.backendAddress + '/api/chat/create', {advertisementId}, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      }),
    });
  }
}
