import {Injectable} from '@angular/core';
import * as signalR from  '@microsoft/signalr';
import {CookieService} from "ngx-cookie-service";
import {HubConnection} from "@microsoft/signalr/dist/esm/HubConnection";

@Injectable({
  providedIn: 'root'
})

export class SignalrService{
  constructor(private cookieService: CookieService) {
  }

  private hubConnection?: signalR.HubConnection;

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/signalr/chat', {
        accessTokenFactory: () => {
          return this.cookieService.get('AuthToken');
        }
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => console.log('Conection started'))
      .catch(err => console.log('Error ', err));
  }

  public on = (methodName: string, handler: (data: any) => void) => {
    this.hubConnection?.on(methodName, data => handler(data));
  }

}
