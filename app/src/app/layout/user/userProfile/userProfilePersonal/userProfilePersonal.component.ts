import { CookieService } from 'ngx-cookie-service';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Dtos/user';

@Component({
    selector: 'app-userProfilePersonal',
    templateUrl: './userProfilePersonal.component.html',
    styleUrls: ['./userProfilePersonal.component.scss'],
})
export class UserProfilePersonalComponent implements OnInit {

    public user: User | null = null;
    userName = "";

    constructor(private userService: UserService, private cookieService: CookieService) { }

    ngOnInit() {
        this.userName = this.cookieService.get('AuthUsername');
        this.loadUserInfo();
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
}
