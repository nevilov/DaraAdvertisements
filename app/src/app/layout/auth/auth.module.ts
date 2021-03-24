import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginPageComponent } from './loginPage/loginPage.component';
import { RegistrationPageComponent } from './registrationPage/registrationPage.component';

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [
    RegistrationPageComponent,
    LoginPageComponent
  ],
  exports: [
    RegistrationPageComponent,
    LoginPageComponent
  ]
})
export class AuthModule { }
