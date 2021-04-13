import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {AppComponent} from "../app.component";

@Injectable({
  providedIn: 'root'
})
export class ChatService{
  constructor(private http: HttpClient) {

  }

  public getChat(advertisementId: number){
    return this.http.get(AppComponent.backendAddress + `api/chat/get/${advertisementId}`);
  }

  public save(advertisementId: number, text: string, customerId?: string){
    return this.http.post(AppComponent.backendAddress + 'api/chat/save', {advertisementId, text, customerId});
  }
}
