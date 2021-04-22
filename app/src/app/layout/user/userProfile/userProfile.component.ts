import { SignService } from './../../../services/sign.service';
import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'app-userProfile',
	templateUrl: './userProfile.component.html',
	styleUrls: ['./userProfile.component.scss'],
})
export class UserProfileComponent implements OnInit {

	public userLogout() {
		this.signService.logout();
	}

	constructor(private signService: SignService) { }

	ngOnInit() { }
}
