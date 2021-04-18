export class ChatResponse{
  chats?: Array<Chat>;
}

export class MessageResponse{
  messages?: Array<ChatMessage>;
}

export class Chat{
  chatId?: number;
  userId?: string;
  cover?: string;
  lastname?: string;
  name?: string;
  updatedDate?: Date;
}

export class ChatMessage{
  text?: string;
  sender?: User;
  recipient?: User;
  createdDate?: Date;
}

export class Message{
  senderId?: string;
  senderName?: string;
  recipientId?: string;
  recipientName?: string;
  text?: string;
  createdDate?: Date;
}

export class SendMessageRequest{
  chatId?: string;
  recipientId?: string;
  Text?: string;
}

export class User{
  id?: string;
  name?: string;
  lastname?: string;
}
