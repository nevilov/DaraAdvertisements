import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-newAbusePage',
  templateUrl: './newAbusePage.component.html',
  styleUrls: ['./newAbusePage.component.scss']
})
export class NewAbusePageComponent implements OnInit {

  abuseForm = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    abuseText: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ])
  });

  onSubmit() {
    console.log("Abuse form info", this.abuseForm.value);
  }

  constructor() { }

  ngOnInit() {
  }

}
