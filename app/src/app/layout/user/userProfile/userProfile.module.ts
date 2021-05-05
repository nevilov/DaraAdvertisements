import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileComponent } from './userProfile.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { UserProfileAdvertisementsComponent } from './userProfileAdvertisements/userProfileAdvertisements.component';
import { UserProfileLoginPassComponent } from './userProfileLoginPass/userProfileLoginPass.component';
import { UserProfilePersonalComponent } from './userProfilePersonal/userProfilePersonal.component';
import { UserProfileSettingsComponent } from './userProfileSettings/userProfileSettings.component';
import {UserProfileBlockComponent} from "./userProfileBlock/userProfileBlock.component";

@NgModule({
	imports: [CommonModule, SharedModule, RouterModule, ReactiveFormsModule],
	declarations: [
		UserProfileComponent,
		UserProfileAdvertisementsComponent,
		UserProfileLoginPassComponent,
		UserProfilePersonalComponent,
		UserProfileSettingsComponent,
    UserProfileBlockComponent
	],
	exports: [
		UserProfileComponent,
		UserProfileAdvertisementsComponent,
		UserProfileLoginPassComponent,
		UserProfilePersonalComponent,
		UserProfileSettingsComponent,
    UserProfileBlockComponent
	],
})
export class UserProfileModule { }
