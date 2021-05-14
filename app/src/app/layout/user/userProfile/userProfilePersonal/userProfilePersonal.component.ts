import { ToastrService } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';
import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Dtos/user';
import { ImageService } from 'src/app/services/image.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { DataSharingService } from 'src/app/services/datasharing.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@UntilDestroy()
@Component({
  selector: 'app-userProfilePersonal',
  templateUrl: './userProfilePersonal.component.html',
  styleUrls: ['./userProfilePersonal.component.scss'],
})
export class UserProfilePersonalComponent implements OnInit {
  public user: User | null = null;
  userName = '';
  avatar: string;
  userImageBlob: string;
  fileToUpload: File;
  isNotificated: boolean;
  isCorporative: boolean;

  userInfoForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    lastName: new FormControl('', Validators.required),
    phone: new FormControl('', [
      Validators.required,
      Validators.pattern('[- +()0-9]+'),
    ]),
  });

  constructor(
    private userService: UserService,
    private cookieService: CookieService,
    private imageService: ImageService,
    private dataSharingService: DataSharingService,
    private toastr: ToastrService
  ) {
    this.avatar = 'default';
    this.fileToUpload = {} as File;
    this.userImageBlob = '';
    this.isNotificated = false;
    this.isCorporative = false;
  }

  ngOnInit() {
    this.userName = this.cookieService.get('AuthUsername');
    this.loadUserInfo();
    this.loadUserAvatar();
  }

  setNotityState(event: any) {
    if (event.currentTarget.checked) {
      this.userService.setNotificationState(true).subscribe((data) => {
        this.toastr.success('Ваша подписка успешно оформлена', 'Успешно!');
      });
    } else {
      this.userService.setNotificationState(false).subscribe((data) => {
        this.toastr.warning('Вы отписались от рассылки', 'Выполнено!');
      });
    }
  }

  onFileChange(event: any) {
    this.fileToUpload = event.target.files[0];
    console.log(this.fileToUpload);

    if (this.fileToUpload != ({} as File)) {
      this.imageService.sendUserAvatarImage(this.fileToUpload).subscribe(
        (data) => {
          this.userService.getUser(this.userName).subscribe((data) => {
            console.log('getUser sucess');
            console.log(data);
            this.cookieService.delete('UserAvatar', '/');
            this.cookieService.set('UserAvatar', data.avatar, 9999, '/');
            this.loadUserAvatar();
            this.dataSharingService.isUserLoggedIn.next(true);
          });
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  loadUserInfo() {
    this.userService.getCurrentUser().subscribe((data) => {
      this.user = data;
      this.isNotificated = Boolean(this.user.isSubscribeToNotifications);
      this.isCorporative = Boolean(this.user.isCorporation);
    });
  }

  avatarChange() {
    const fileInput = document.getElementById('avatar');
    fileInput?.addEventListener('change', function () {
      const selectedFile = fileInput;
    });
  }

  loadUserAvatar() {
    this.avatar = this.cookieService.get('UserAvatar');
    if (this.avatar == 'null') {
      this.avatar = 'default';
    }

    this.imageService
      .getImageById(this.avatar)
      .pipe(untilDestroyed(this))
      .subscribe((data: any) => {
        if (data.imageBlob) {
          this.userImageBlob = 'data:image/jpeg;base64,' + data.imageBlob;
        }
      });
  }

  onSubmit() {
    this.userService.updateUserInfo(this.userInfoForm.value).subscribe(
      (res) => {
        this.toastr.success('Информация обновлена.', 'Успешно!');
        this.loadUserInfo();
      },
      (error) => {
        this.toastr.error('Проверте заполенение полей', 'Ошибка!');
      }
    );
  }
}
