import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { SignalrService } from '../../../services/signalr.service';
import { ChatService } from '../../../services/chat.service';
import { CookieService } from 'ngx-cookie-service';
import { Chat, ChatMessage, Message } from '../../../Dtos/chat';
import { UserService } from '../../../services/user.service';
import {ImageService} from "../../../services/image.service";
import {UntilDestroy, untilDestroyed} from "@ngneat/until-destroy";
@UntilDestroy()
@Component({
  selector: 'app-userChats',
  templateUrl: './userChats.component.html',
  styleUrls: ['./userChats.component.scss'],
})
export class UserChatsComponent implements OnInit {
  userId = '';
  currentChatId = 0;
  currentRecipientId = ' ';
  isSeller = false;
  chats: Chat[] = [];
  messages: ChatMessage[] = [];

  isMessageInputShown = false;

  @ViewChild('seller', { read: ElementRef }) Seller!: ElementRef;
  @ViewChild('customer', { read: ElementRef }) Customer!: ElementRef;
  @ViewChild('chat', { read: ElementRef }) Chat!: ElementRef;
  @ViewChild('messageText', { read: ElementRef }) MessageInput!: ElementRef;

  constructor(
    private signalrService: SignalrService,
    private chatService: ChatService,
    private cookieService: CookieService,
    private userService: UserService,
    private imageService: ImageService
  ) {}

  ngOnInit() {
    this.signalrService.startConnection();
    this.signalrService.on('message', (message: Message) =>
      this._handleMessage(message)
    );

    this.getChats();

    const username = this.cookieService.get('AuthUsername');

    this.userService.getCurrentUser().subscribe((data) => {
      this.userId = data.id;
    });
  }

  private _handleMessage = (message: Message) => {
    const chatMessage = {
      text: message.text,
      senderName: message.senderName,
      createdDate: message.createdDate,
      sender: { id: message.senderId },
      recipient: { id: message.recipientId },
    } as ChatMessage;
    this.messages.push(chatMessage);
  };

  getChatMessages(chatId: number): void {
    this.currentChatId = chatId;
    this.chatService.getMessage(chatId).subscribe((data) => {
      this.messages = data.messages!;
    });
    this.isMessageInputShown = true;
  }

  sendMessage(text: string) {
    this.chatService
      .sendMessage(this.currentChatId, this.currentRecipientId, text)
      .subscribe();
    this.Chat.nativeElement.scrollTo(0, this.Chat.nativeElement.scrollHeight);
    this.MessageInput.nativeElement.value = '';
  }

  getCurrentRecipient(recipientId: string) {
    this.currentRecipientId = recipientId;
  }

  getChats() {
    this.chatService.getChats(this.isSeller).subscribe((data) => {
      this.chats = data.chats!;
      console.log(this.chats);
      for (let i = 0; i < this.chats.length; i++) {
        if (this.chats[i].avatar === null) {
          this.chats[i].avatar = 'default';
        }

        this.imageService
          .getImageById(this.chats[i].avatar!)
          .pipe(untilDestroyed(this))
          .subscribe((data: any) => {
            console.log(this.chats[i].avatar!);
            if (data.imageBlob) {
              this.chats[i].avatar =
                'data:image/jpeg;base64,' + data.imageBlob;
            }
          });
      }
    });
  }

  chatSeller() {
    this.isSeller = true;
    this.getChats();
    this.Seller.nativeElement.parentElement.classList.add(
      'container__tab-active'
    );
    this.Customer.nativeElement.parentElement.classList.remove(
      'container__tab-active'
    );
  }

  chatRecipient() {
    this.isSeller = false;
    this.getChats();
    this.Customer.nativeElement.parentElement.classList.add(
      'container__tab-active'
    );
    this.Seller.nativeElement.parentElement.classList.remove(
      'container__tab-active'
    );
  }
}
