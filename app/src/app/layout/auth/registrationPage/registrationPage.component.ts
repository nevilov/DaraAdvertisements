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

    registrationState: string = "Регистрация";
    isClickAllowed: boolean = true;
    passwordRegex: RegExp = new RegExp('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})');
    emailRegex: RegExp = new RegExp('.{1,}@[^.]{1,}');
    phoneRegex: RegExp = new RegExp('[- +()0-9]+');
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
        if (this.isClickAllowed = true) {
            this.isClickAllowed = false;
            const formValue = this.registrationForm.value;
            this.registrationState = "Идёт регистрация..."
            this.sub = this.signService.register(formValue).pipe(untilDestroyed(this)).subscribe(() => {
                this.registrationState = "Успешно. Проверьте свою почту.";
                this.router.navigateByUrl('/autorization');
            }, (error) => { 
                this.registrationState = "Ошибка. Проверьте поля и нажмите ещё раз.";
                this.isClickAllowed = true;
            });
        }
    }

    constructor(private signService: SignService, private router: Router) {
        this.sub = new Subscription;
    }

    ngOnInit() {
    }

}
