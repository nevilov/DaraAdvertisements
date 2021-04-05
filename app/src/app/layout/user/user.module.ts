import { RouterModule } from '@angular/router';
import { PublicProfileComponent } from './publicProfile/publicProfile.component';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileComponent } from './userProfile/userProfile.component';
import { UserPrivateChatComponent } from './userPrivateChat/userPrivateChat.component';
import { UserChatsComponent } from './userChats/userChats.component';

@NgModule({
  imports: [CommonModule, SharedModule, RouterModule],
  declarations: [
    UserProfileComponent,
    UserPrivateChatComponent,
    UserChatsComponent,
    PublicProfileComponent,
  ],
  exports: [UserProfileComponent, UserPrivateChatComponent, UserChatsComponent],
})
export class UserModule {}
