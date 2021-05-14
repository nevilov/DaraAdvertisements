import { Component } from '@angular/core';
import { EmailValidator, FormControl, FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
import { SignService } from "../../../services/sign.service";
import { UntilDestroy, untilDestroyed } from "@ngneat/until-destroy";
import { resetPassword } from "../../../Dtos/resetPassword";
import {ToastrService} from "ngx-toastr";

@UntilDestroy()
@Component({
    selector: 'app-resetPasswordPage',
    templateUrl: './resetPasswordPage.component.html',
    styleUrls: ['./resetPasswordPage.component.scss']
})

export class ResetPasswordPageComponent {
    private sub: Subscription;
    private token: string = '';
    private userId: string = '';

    resetPasswordForm = new FormGroup({
        newPassword: new FormControl(),
        repeatedPassword: new FormControl()
    });

    onSubmit(): void {
        const formValue = this.resetPasswordForm.value;

        const resetPasswordRequest: resetPassword = {
            userId: this.userId,
            token: this.token,
            newPassword: formValue.newPassword,
            repeatedPassword: formValue.repeatedPassword
        };

        console.log(resetPasswordRequest);
        this.sub = this.service.resetPassword(resetPasswordRequest).pipe(untilDestroyed(this)).subscribe(() => {
            this.toastrService.success('Пароль успешно изменен', 'Восстановление пароля')
            this.router.navigateByUrl('/');
        });
    }

    constructor(private route: ActivatedRoute, private service: SignService,
                private router: Router,
                private toastrService: ToastrService) {
        this.sub = new Subscription;
    }

    ngOnInit(): void {
        this.token = this.route.snapshot.queryParams['token'];
        this.userId = this.route.snapshot.queryParams['userId'];
    }
}
