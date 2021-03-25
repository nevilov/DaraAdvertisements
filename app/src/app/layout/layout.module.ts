import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserModule } from './user/user.module';
import { AuthModule } from './auth/auth.module';
import { AdvertisementModule } from './advertisement/advertisement.module';
import { AbuseModule } from './abuse/abuse.module';

@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    UserModule,
    AuthModule,
    AdvertisementModule,
    AbuseModule,
    SharedModule
  ]
})
export class LayoutModule { }