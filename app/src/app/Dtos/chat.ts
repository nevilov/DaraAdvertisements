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
  avatar?: string;
  name?: string;
  updatedDate?: Date;
  advertisement?: Advertisement;
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

export class User{
  id?: string;
  name?: string;
  lastname?: string;
}


export class Advertisement{
   id?: number;
   title?: string;
}
