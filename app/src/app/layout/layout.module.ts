import { MatExpansionModule } from '@angular/material/expansion';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserModule } from './user/user.module';
import { AuthModule } from './auth/auth.module';
import { AdvertisementModule } from './advertisement/advertisement.module';
import { AbuseModule } from './abuse/abuse.module';
import { HelpComponent } from './help/help.component';
import { ContactsComponent } from './contacts/contacts.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';

@NgModule({
    declarations: [
        HelpComponent,
        ContactsComponent
    ],
    imports: [
        CommonModule,
        UserModule,
        AuthModule,
        AdvertisementModule,
        AbuseModule,
        SharedModule,
        MatExpansionModule,
        BrowserAnimationsModule,
        MatCardModule,
    ]
})
export class LayoutModule { }