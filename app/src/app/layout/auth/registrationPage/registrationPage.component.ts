import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-registrationPage',
  templateUrl: './registrationPage.component.html',
  styleUrls: ['./registrationPage.component.scss']
})
export class RegistrationPageComponent implements OnInit {

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
    console.log("User registration info", this.registrationForm.value);
  }

  constructor() { }

  ngOnInit() {
  }

}
