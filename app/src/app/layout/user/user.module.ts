import { UserProfileModule } from './userProfile/userProfile.module';
import { RouterModule } from '@angular/router';
import { PublicProfileComponent } from './publicProfile/publicProfile.component';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserPrivateChatComponent } from './userPrivateChat/userPrivateChat.component';
import { UserChatsComponent } from './userChats/userChats.component';

@NgModule({
	imports: [CommonModule, SharedModule, RouterModule, UserProfileModule],
	declarations: [
		UserPrivateChatComponent,
		UserChatsComponent,
		PublicProfileComponent,
	],
	exports: [
		UserPrivateChatComponent,
		UserChatsComponent,
	],
})
export class UserModule { }
