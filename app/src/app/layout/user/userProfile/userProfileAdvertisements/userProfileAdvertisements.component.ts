import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
	selector: 'app-userProfileAdvertisements',
	templateUrl: './userProfileAdvertisements.component.html',
	styleUrls: ['./userProfileAdvertisements.component.scss'],
})
export class UserProfileAdvertisementsComponent implements OnInit {
	advertisements: Advertisement[] = [];

	userName: string = '';
	constructor(
		private userService: UserService,
		private cookieService: CookieService
	) { }

	ngOnInit() {
		this.userName = this.cookieService.get('AuthUsername');
		this.loadUserInfo();
	}

	loadAdvertisements(id: string) {
		this.userService.getUserAdvertisements(id).subscribe((data) => {
			this.advertisements = data.items;
			console.log(data);
		});
	}

	loadUserInfo() {
		this.userService.getUser(this.userName).subscribe((data) => {
			this.loadAdvertisements(data.id);
			console.log(data);
		});
	}
}
