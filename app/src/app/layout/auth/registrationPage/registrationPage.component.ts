import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SignService } from './../../../services/sign.service';

@Component({
  selector: 'app-registrationPage',
  templateUrl: './registrationPage.component.html',
  styleUrls: ['./registrationPage.component.scss']
})
export class RegistrationPageComponent implements OnInit, OnDestroy {

  private sub: Subscription;

  registrationForm = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    email: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6)
    ]),
  });

  onSubmit() {
    const formValue = this.registrationForm.value;
    //TODO REMOVE AFTER FIX BACK
    formValue.username = formValue.name;
    formValue.lastName = formValue.name;
//    console.log("User registration info", this.registrationForm.value);

    this.sub = this.signService.register(formValue).subscribe(() => {
      this.router.navigateByUrl('/autorization');
    });

  }

  constructor(private signService: SignService, private router: Router) {
    this.sub = new Subscription;
   }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

}