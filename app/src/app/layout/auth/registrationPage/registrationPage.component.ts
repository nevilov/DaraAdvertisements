import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Subscription } from 'rxjs';
import { SignService } from './../../../services/sign.service';

@UntilDestroy()
@Component({
  selector: 'app-registrationPage',
  templateUrl: './registrationPage.component.html',
  styleUrls: ['./registrationPage.component.scss']
})
export class RegistrationPageComponent implements OnInit {

  private sub: Subscription;

  registrationForm = new FormGroup({
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),

    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6)
    ]),
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    lastName: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    email: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    phone: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.pattern('[- +()0-9]+')
    ])
  });

  onSubmit() {
    const formValue = this.registrationForm.value;

    this.sub = this.signService.register(formValue).pipe(untilDestroyed(this)).subscribe(() => {
      this.router.navigateByUrl('/autorization');
    });

  }

  constructor(private signService: SignService, private router: Router) {
    this.sub = new Subscription;
   }

  ngOnInit() {
  }

}
