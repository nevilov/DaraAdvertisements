import { NULL_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import {SignalrService} from '../../../services/signalr.service';
import {ChatService} from '../../../services/chat.service';
import {Chat, ChatMessage, ChatResponse, Message} from '../../../Dtos/chat';
import jwtDecode from "jwt-decode";
import {CookieService} from "ngx-cookie-service";

@Component({
  selector: 'app-advertisementDetailPage',
  templateUrl: './advertisementDetailPage.component.html',
  styleUrls: ['./advertisementDetailPage.component.scss'],
  providers: [
    AdvertisementService
  ]
})
export class AdvertisementDetailPageComponent implements OnInit {
    public id = 0;
    public advertisement: Advertisement | null = null;
    public chats: Array<Chat> = [];

    constructor(private route: ActivatedRoute,
                private advertisementService: AdvertisementService,
                private signalrService: SignalrService,
                private chatService: ChatService,
                private cookieService: CookieService)
    {
    }

    ngOnInit() {
        this.route.paramMap.pipe(
            switchMap(params => params.getAll('id'))
        )
        .subscribe(data => this.id = +data);
        console.log(this.id);
        this.advertisementService.getAdvertisementById(this.id)
            .subscribe((data: Advertisement) => {
              this.advertisement = data;
              console.log(data);
            });

        this.signalrService.startConnection();
        this.signalrService.on('message', (message: Message) => this._handlerMessage(message));


        this.chatService.getChat(this.id).subscribe(({chats}: ChatResponse) => {
          this.chats = chats!;
        });
    }

    public sendMessage(text: string, customerId: string | undefined){
      this.chatService.save(this.id, text, customerId);
    }

  public isNotAuthor() {
    if (!this.advertisement) return false;

    let token: any = jwtDecode<object>(this.cookieService.get('AuthToken'));

    for (const key in token) {
      if (key.includes('nameidentifier')) {
        return token[key] !== this.advertisement.owner.id;
      }
    }

    return true;
  }

    private _handlerMessage = (message: Message) => {
      let chatMessage = {
        text: message.text,
        senderName: message.senderName,
        created: message.createdDate
      } as ChatMessage;

      if (this.chats.length) {
        let chat = this.chats?.find(c => c.customerId === message.customerId);
        if (chat) {
          chat.messages = [chatMessage, ...chat.messages];
          return;
        }
      }

      this.chats.push( {
        customerId: message.customerId,
        customerName: message.customerName,
        messages: [chatMessage]
      } as Chat);
    }
}
