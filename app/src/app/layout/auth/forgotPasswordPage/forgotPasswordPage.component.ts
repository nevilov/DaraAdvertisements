import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {SignService} from "../../../services/sign.service";
import {Subscription} from "rxjs";
import {UntilDestroy, untilDestroyed} from '@ngneat/until-destroy';
import {ToastrService} from "ngx-toastr";

@UntilDestroy()
@Component({
  selector: 'app-forgotPassword',
  templateUrl: './forgotPasswordPage.component.html',
  styleUrls: ['./forgotPasswordPage.component.scss']
})
export class ForgotPasswordPageComponent implements OnInit {
  private sub: Subscription;


  forgotPasswordForm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.email
    ]),
  });

  onSubmit(): void{
    const formValue = this.forgotPasswordForm.value.email;
    this.sub = this.signService.forgotPassword(formValue).pipe(untilDestroyed(this)).subscribe(() => {
      this.toastrService.success('Письмо о смене пароля отправлено на почту', 'Восстановление пароля')
      this.router.navigateByUrl('/');
    });
  }

  ngOnInit(): void {
  }

  constructor(private signService: SignService,
              private router: Router,
              private toastrService: ToastrService) {
    this.sub = new Subscription;
  }

}
