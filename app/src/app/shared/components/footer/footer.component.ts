import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
	selector: 'app-footer',
	templateUrl: './footer.component.html',
	styleUrls: ['./../../../../assets/scss/layout/__footer.scss']
})
export class FooterComponent implements OnInit {

	MailingForm: FormGroup;

	constructor() {
		this.MailingForm = new FormGroup({
			email: new FormControl()
		});
	}

	ngOnInit() {
	}

	onSubscribe(): void {
		console.log(this.MailingForm.value.email);
	}


}
