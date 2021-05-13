import { HttpErrorResponse } from '@angular/common/http';
import { UserLoginDto } from './../../../Dtos/UserLoginDto';
import { catchError } from 'rxjs/operators';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SignService } from './../../../services/sign.service';
@UntilDestroy()
@Component({
  selector: 'app-loginPage',
  templateUrl: './loginPage.component.html',
  styleUrls: ['./loginPage.component.scss'],
})
export class LoginPageComponent implements OnInit {
  private sub: Subscription;

  autorizeForm = new FormGroup({
    login: new FormControl('', [Validators.required, Validators.minLength(5)]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
  });

  error: string = '';

  onSubmit() {
    const user: UserLoginDto = {
      login: this.autorizeForm.value.login,
      password: this.autorizeForm.value.password,
    };
    this.signService.login(user).subscribe(
      (data) => {
        console.log(data);
        this.toastr.success('Вы успешно авторизованы', 'Успешно!');
        this.router.navigateByUrl('/');
      },
      (error: HttpErrorResponse) => {
        this.toastr.error(error.error.error, 'Ошибка!');
      }
    );
  }

  constructor(
    private signService: SignService,
    private router: Router,
    public toastr: ToastrService
  ) {
    this.sub = new Subscription();
  }

  ngOnInit() {}
}
