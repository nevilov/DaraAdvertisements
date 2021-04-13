export class ChatResponse{
  chats?: Array<Chat>;
}

export interface Chat{
  customerId: string;
  customerName: string;
  messages: Array<ChatMessage>;
}

export class ChatMessage{
  text?: string;
  senderName?: string;
  createdDate?: Date;
}

export class Message{
  text?: string;
  senderName?: string;
  createdDate?: Date;
  customerId?: string;
  customerName?: string;
}
