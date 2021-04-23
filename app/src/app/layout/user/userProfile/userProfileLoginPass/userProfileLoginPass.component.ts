import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { untilDestroyed } from '@ngneat/until-destroy';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/services/user.service';

@Component({
	selector: 'app-userProfileLoginPass',
	templateUrl: './userProfileLoginPass.component.html',
	styleUrls: ['./userProfileLoginPass.component.scss'],
	providers: [UserService],
})
export class UserProfileLoginPassComponent implements OnInit {
	isEmailChanges = false;
	isPasswordChanges = false;
	private sub: Subscription;

	constructor(private userService: UserService) {
		this.sub = new Subscription();
	}

	changePasswordForm = new FormGroup({
		newPassword: new FormControl('', [
			Validators.required,
			Validators.minLength(6),
		]),
		oldPassword: new FormControl('', [
			Validators.required,
			Validators.minLength(6),
		]),
		repeatedNewPassword: new FormControl('', [
			Validators.required,
			Validators.minLength(6),
		]),
	});

	changeEmailForm = new FormGroup({
		email: new FormControl('', [Validators.required, Validators.minLength(5)]),
	});

	onSubmit(form: string) {
		const formValue = this.changePasswordForm.value;

		console.log(formValue);
		if (form == 'password') {
			this.sub = this.userService.changePassword(formValue).subscribe(() => {
				// this.router.navigateByUrl('/autorization');
			});
		} else if (form == 'email') {
		}
	}

	ngOnInit() { }
}
