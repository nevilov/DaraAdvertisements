import { Component, OnInit } from '@angular/core';
import {SignalrService} from "../../../services/signalr.service";
import {ChatService} from "../../../services/chat.service";
import {CookieService} from "ngx-cookie-service";
import {Advertisement} from "../../../Dtos/advertisement";
import {Chat, ChatMessage, Message} from "../../../Dtos/chat";

@Component({
  selector: 'app-userChats',
  templateUrl: './userChats.component.html',
  styleUrls: ['./userChats.component.scss']
})
export class UserChatsComponent implements OnInit {

  Chats: Chat[] = [];
  constructor(private signalrService: SignalrService,
              private chatService: ChatService,
              private cookieService: CookieService)
  { }

  ngOnInit() {
    this.signalrService.startConnection();
    this.signalrService.on('message', (message: Message) => this._handleMessage(message));

    this.chatService.getChats(false).subscribe((data) => {
      this.Chats = data.chats!;
      console.log(this.Chats);
    });
  }

  private _handleMessage = (message: Message) => {
    const chatMessage = <ChatMessage>{
      text: message.text,
      senderName: message.senderName,
      created: message.createdDate
    }
  }

  getChatMessages(): void{

  }

}
