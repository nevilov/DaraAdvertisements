import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup} from "@angular/forms";
import {NotificationService} from "../../../../services/notification.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";


@Component({
  selector: 'app-userProfileSendNotifications',
  templateUrl: './userProfileSendNotifications.component.html',
  styleUrls: ['./userProfileSendNotifications.component.scss']
})

export class UserProfileSendNotificationsComponent implements OnInit{
  public isDisabled: boolean = false;

  constructor(private notificationService: NotificationService,
              private toastrService: ToastrService,
              private router: Router) {
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
        this.toastrService.success('Рассылка отправлена');
        return this.router.navigateByUrl('/');
    });
  }

}
