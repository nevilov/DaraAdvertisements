import {Component} from "@angular/core";
import {FormControl, FormGroup} from "@angular/forms";
import {UserService} from "../../../../services/user.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";


@Component({
  selector: 'app-userProfileChangeUserStatus',
  templateUrl: './userProfileChangeUserStatus.component.html',
  styleUrls: ['./userProfileChangeUserStatus.component.scss']
})

export class UserProfileChangeUserStatusComponent {
  constructor(private userService: UserService,
              private router: Router,
              private toastrService: ToastrService) {
  }

  changeCorporationForm = new FormGroup({
    userId: new FormControl(),
    isCorporation: new FormControl()
  });

  onSend(){
    this.userService.changeCorporationStatus(this.changeCorporationForm.value.userId,
      this.changeCorporationForm.value.isCorporation).subscribe(a => {
      this.toastrService.success('Статус пользователя изменен');
      return this.router.navigateByUrl('/cabinet/personal');
    });
  }
}
