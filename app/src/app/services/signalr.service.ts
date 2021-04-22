import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { CookieService } from "ngx-cookie-service";

@Injectable({
    providedIn: 'root'
})

export class SignalrService {
    constructor(private cookieService: CookieService) {
    }

    private hubConnection: signalR.HubConnection | undefined;

    public startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:5001/signalr/chat", {
                accessTokenFactory: () => {
                    return this.cookieService.get('AuthToken');
                }
            })
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ', err));
    }

    public on = (methodName: string, handler: (data: any) => void) => {
        this.hubConnection?.on(methodName, data => handler(data));
    }
}
