import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { untilDestroyed } from '@ngneat/until-destroy';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-userProfileLoginPass',
  templateUrl: './userProfileLoginPass.component.html',
  styleUrls: ['./userProfileLoginPass.component.scss'],
  providers: [UserService],
})
export class UserProfileLoginPassComponent implements OnInit {
  isEmailChanges = false;
  isPasswordChanges = false;
  private sub: Subscription;

  constructor(
    private userService: UserService,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.sub = new Subscription();
  }

  changePasswordForm = new FormGroup({
    newPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
    oldPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
    repeatedNewPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
  });

  changeEmailForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.minLength(5)]),
  });

  onChangePassword() {
    this.sub = this.userService
      .changePassword(this.changePasswordForm.value)
      .subscribe(() => {
        this.toastr.success('Пароль успешно изменен', 'Изменение пароля');
        this.router.navigateByUrl('/cabinet/personal');
      });
  }

  onChangeEmail() {
    this.userService
      .changeEmail(this.changeEmailForm.value.email)
      .subscribe(() => {
        this.toastr.success(
          'Проверте новую почту для подтверждения',
          'Изменение почты'
        );
        this.router.navigateByUrl('/cabinet/personal');
      });
  }

  ngOnInit() {}
}
