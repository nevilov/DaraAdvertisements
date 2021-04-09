import {Component} from '@angular/core';
import {EmailValidator, FormControl, FormGroup} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {SignService} from "../../../services/sign.service";
import {UntilDestroy, untilDestroyed} from "@ngneat/until-destroy";
import {resetPassword} from "../../../Dtos/resetPassword";

@UntilDestroy()
@Component({
  selector: 'app-resetPasswordPage',
  templateUrl: './resetPasswordPage.component.html',
  styleUrls: ['./resetPasswordPage.component.scss']
})

export class ResetPasswordPageComponent{
  private sub: Subscription;
  private resetPasswordRequest: resetPassword | undefined;
  private token: string = ' s';
  private userId: string = ' s';

  resetPasswordForm = new FormGroup({
    newPassword: new FormControl(),
    repeatedPassword: new FormControl()
  });

  onSubmit(): void
  {
    const formValue = this.resetPasswordForm.value;
    this.resetPasswordRequest = {
      userId: this.userId,
      token: this.token,
      newPassword: formValue.newPassword,
      repeatedPassword: formValue.repeatedPassword
    };
    console.log(this.resetPasswordRequest);
    this.sub = this.service.resetPassword(this.resetPasswordRequest).pipe(untilDestroyed(this)).subscribe(() => {
      this.router.navigateByUrl('/');
    });
  }

  constructor(private route: ActivatedRoute, private service: SignService, private router: Router) {
    this.sub = new Subscription;
  }

  ngOnInit(): void{
    this.token = this.route.snapshot.queryParams['token'];
    this.userId = this.route.snapshot.queryParams['userId'];
  }
}
