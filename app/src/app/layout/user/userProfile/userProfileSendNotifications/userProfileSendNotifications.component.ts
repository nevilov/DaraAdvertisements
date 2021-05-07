import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup} from "@angular/forms";
import {NotificationService} from "../../../../services/notification.service";
import {Router} from "@angular/router";


@Component({
  selector: 'app-userProfileSendNotifications',
  templateUrl: './userProfileSendNotifications.component.html',
  styleUrls: ['./userProfileSendNotifications.component.scss']
})

export class UserProfileSendNotificationsComponent implements OnInit{
  public isDisabled: boolean = false;

  constructor(private notificationService: NotificationService, private router: Router) {
  }
  notificationForm = new FormGroup({
    subject: new FormControl(),
    message: new FormControl()
  });

  ngOnInit(): void {
  }

  onSend(){
    this.isDisabled = true;
    this.notificationService.send(this.notificationForm.value.subject, this.notificationForm.value.message)
      .subscribe(a => {
      return this.router.navigateByUrl('/');
    });
  }

}
