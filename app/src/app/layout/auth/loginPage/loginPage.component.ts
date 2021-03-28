import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SignService } from './../../../services/sign.service';

@Component({
  selector: 'app-loginPage',
  templateUrl: './loginPage.component.html',
  styleUrls: ['./loginPage.component.scss']
})
export class LoginPageComponent implements OnInit, OnDestroy {

  private sub: Subscription;

  autorizeForm = new FormGroup({
    login: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6)
    ]),
  });

  onSubmit() {
    const formValue = this.autorizeForm.value;
//    console.log("User autorize info", this.autorizeForm.value);
    this.sub = this.signService.login(formValue).subscribe(() => {
      this.router.navigateByUrl('/');
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
