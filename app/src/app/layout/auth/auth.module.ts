import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginPageComponent } from './loginPage/loginPage.component';
import { RegistrationPageComponent } from './registrationPage/registrationPage.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule
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
