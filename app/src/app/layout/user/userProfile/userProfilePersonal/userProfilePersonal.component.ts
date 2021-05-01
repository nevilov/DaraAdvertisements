import { CookieService } from 'ngx-cookie-service';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Dtos/user';
import { ImageService } from 'src/app/services/image.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { DataSharingService } from 'src/app/services/datasharing.service';

@UntilDestroy()
@Component({
    selector: 'app-userProfilePersonal',
    templateUrl: './userProfilePersonal.component.html',
    styleUrls: ['./userProfilePersonal.component.scss'],
})
export class UserProfilePersonalComponent implements OnInit {

    public user: User | null = null;
    userName = "";
    avatar: string;
    userImageBlob: string;
    fileToUpload: File;

    constructor(
        private userService: UserService,
        private cookieService: CookieService,
        private imageService: ImageService,
        private dataSharingService: DataSharingService) { 
        this.avatar = "default";
        this.fileToUpload = {} as File;
        this.userImageBlob = "";
    }

    ngOnInit() {
        this.userName = this.cookieService.get('AuthUsername');
        this.loadUserInfo();
        this.loadUserAvatar();
    }

    onFileChange(event: any) {
        this.fileToUpload = event.target.files[0];
        console.log(this.fileToUpload);

        if (this.fileToUpload != {} as File) {
            this.imageService.sendUserAvatarImage(this.fileToUpload).subscribe(data => {
                this.userService.getUser(this.userName).subscribe(data => {
                    console.log('getUser sucess');
                    console.log(data);
                    this.cookieService.delete('UserAvatar', '/');
                    this.cookieService.set('UserAvatar', data.avatar, 9999, '/');
                    this.loadUserAvatar();
                    this.dataSharingService.isUserLoggedIn.next(true);
                });
                }, error => {
                  console.log(error);
                });
        }
    }

    loadUserInfo() {
        this.userService.getUser(this.userName).subscribe((data) => {
            this.user = data;
            console.log(data);
        });
    }

    avatarChange() {
        const fileInput = document.getElementById('avatar');
        fileInput?.addEventListener('change', function () {
            const selectedFile = fileInput;
            alert();
        });
    }

    loadUserAvatar() {
        this.avatar = this.cookieService.get('UserAvatar');

        if (this.avatar == "null") {
            this.avatar = "default";
        }

        this.imageService.getImageById(this.avatar)
        .pipe(untilDestroyed(this))
        .subscribe((data: any) => {
            if (data.imageBlob) {
                this.userImageBlob = 'data:image/jpeg;base64,' + data.imageBlob;
            }
        });
    };
    
}
