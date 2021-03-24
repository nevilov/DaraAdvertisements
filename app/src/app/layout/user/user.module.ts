import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileComponent } from './userProfile/userProfile.component';
import { UserPrivateCharComponent } from './userPrivateChar/userPrivateChar.component';
import { UserChatsComponent } from './userChats/userChats.component';

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [
    UserProfileComponent,
    UserPrivateCharComponent,
    UserChatsComponent
  ],
  exports: [
    UserProfileComponent,
    UserPrivateCharComponent,
    UserChatsComponent
  ]
})
export class UserModule { }
