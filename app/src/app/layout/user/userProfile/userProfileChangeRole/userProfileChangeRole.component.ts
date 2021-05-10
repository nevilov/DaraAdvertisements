import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import {UserService} from "../../../../services/user.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-userProfileChangeRole',
  templateUrl: './userProfileChangeRole.component.html',
  styleUrls: ['./userProfileChangeRole.component.scss']
})

export class UserProfileChangeRoleComponent{

  changeRoleForm = new FormGroup({
  userId: new FormControl(),
  newRole: new FormControl(),
  });

  constructor(private userService: UserService,
              private toastrService: ToastrService,
              private router: Router) {
  }

  onSend(){
    this.userService.changeRole(this.changeRoleForm.value).subscribe(a => {
      this.toastrService.success('Роль пользователя изменена');
      return this.router.navigateByUrl('/cabinet/personal');
    });
  }


}
