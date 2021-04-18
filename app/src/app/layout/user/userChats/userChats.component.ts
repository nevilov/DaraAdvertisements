import { Component, OnInit } from '@angular/core';
import {SignalrService} from "../../../services/signalr.service";
import {ChatService} from "../../../services/chat.service";
import {CookieService} from "ngx-cookie-service";
import {Chat, ChatMessage, Message, MessageResponse} from "../../../Dtos/chat";

@Component({
  selector: 'app-userChats',
  templateUrl: './userChats.component.html',
  styleUrls: ['./userChats.component.scss']
})
export class UserChatsComponent implements OnInit {
  userId: string = '';
  chats: Chat[] = [];
  messages: ChatMessage[] = [];
  constructor(private signalrService: SignalrService,
              private chatService: ChatService,
              private cookieService: CookieService)
  { }

  ngOnInit() {
    this.signalrService.startConnection();
    this.signalrService.on('message', (message: Message) => this._handleMessage(message));

    this.chatService.getChats(false).subscribe((data) => {
      this.chats = data.chats!;
      console.log(this.chats);
    });
  }

  private _handleMessage = (message: Message) => {
    const chatMessage = <ChatMessage>{
      text: message.text,
      senderName: message.senderName,
      created: message.createdDate
    }
  }

  getChatMessages(chatId: number): void{
    this.chatService.getMessage(chatId).subscribe((data)=>{
      this.messages = data.messages!;
    });

  }

}
