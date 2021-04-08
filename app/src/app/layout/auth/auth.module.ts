import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginPageComponent } from './loginPage/loginPage.component';
import { RegistrationPageComponent } from './registrationPage/registrationPage.component';
import { ForgotPasswordPageComponent } from './forgotPasswordPage/forgotPasswordPage.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule
  ],
  declarations: [
    RegistrationPageComponent,
    LoginPageComponent,
    ForgotPasswordPageComponent
  ],
  exports: [
    RegistrationPageComponent,
    LoginPageComponent,
    ForgotPasswordPageComponent
  ]
})
export class AuthModule { }
