import { CookieService } from 'ngx-cookie-service';
import { SignService } from './../../../services/sign.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-userProfile',
    templateUrl: './userProfile.component.html',
    styleUrls: ['./userProfile.component.scss'],
})
export class UserProfileComponent implements OnInit {

    public userRole = "";

    public userLogout() {
        this.signService.logout();
    }

    constructor(private signService: SignService, private cookieService: CookieService) { }

    ngOnInit() {
        this.cookieService.deleteAll('/cabinet');  // TODO Быстрое решение дублирования кук из кабинета
        this.userRole = this.cookieService.get('UserRole');
    }
}
