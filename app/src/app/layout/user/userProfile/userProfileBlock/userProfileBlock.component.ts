import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {UserService} from "../../../../services/user.service";

@Component({
  selector: 'app-userProfileBlock',
  templateUrl: './userProfileBlock.component.html',
  styleUrls: ['./userProfileBlock.component.scss']
})

export class UserProfileBlockComponent implements OnInit{
  blockUserForm: FormGroup = new FormGroup({
    userId: new FormControl('', [Validators.required, Validators.minLength(5)]),
    untilDate: new FormControl()
  });

  constructor(private userService: UserService) {
  }

  ngOnInit(): void {
  }

  onSubmit(){
    this.userService.blockUser(this.blockUserForm.value).subscribe();
  }
}
