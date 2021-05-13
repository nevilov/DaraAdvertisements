import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-userProfileBlock',
  templateUrl: './userProfileBlock.component.html',
  styleUrls: ['./userProfileBlock.component.scss'],
})
export class UserProfileBlockComponent implements OnInit {
  blockUserForm: FormGroup = new FormGroup({
    userId: new FormControl('', [Validators.required, Validators.minLength(5)]),
    untilDate: new FormControl(),
  });

  constructor(
    private userService: UserService,
    private toastrService: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  onSubmit() {
    this.userService.blockUser(this.blockUserForm.value).subscribe((a) => {
      this.toastrService.success('Пользователь заблокирован');
      return this.router.navigateByUrl('/cabinet/personal');
    });
  }
}
