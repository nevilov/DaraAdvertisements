import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {AppComponent} from "../app.component";
import {CookieService} from "ngx-cookie-service";
import {Observable} from "rxjs";
import {ChatResponse, MessageResponse} from "../Dtos/chat";

@Injectable({
  providedIn: 'root'
})
export class ChatService{
  constructor(private http: HttpClient, private cookieService: CookieService) {

  }

  public getMessage(chatId: number): Observable<MessageResponse>{
    return this.http.get<MessageResponse>(AppComponent.backendAddress + '/api/message/get/' + chatId, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      })
    });
  }

  public sendMessage(chatId: number, recipientId: string, text: string){
    return this.http.post(AppComponent.backendAddress + '/api/message/send', {chatId, recipientId, text}, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookieService.get('AuthToken'),
      })
    });
  }

  public getChats(isSeller: boolean): Observable<ChatResponse>{
    return this.http.get<ChatResponse>(AppComponent.backendAddress + `/api/chat/get/${isSeller}`,{
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
