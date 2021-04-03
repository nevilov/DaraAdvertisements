import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { NewAbuse } from 'src/app/Dtos/abuse';
import { AbuseService } from './../../../services/abuse.service';

@UntilDestroy()
@Component({
  selector: 'app-newAbusePage',
  templateUrl: './newAbusePage.component.html',
  styleUrls: ['./newAbusePage.component.scss']
})
export class NewAbusePageComponent implements OnInit {

  abuseForm = new FormGroup({
    advId: new FormControl('', [
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
    console.log("add called!");

    const abuse: NewAbuse = {
      advId: this.abuseForm.value.advId,
      abuseText: this.abuseForm.value.abuseText,
    };

    this.abuseService.createAbuse(abuse)
    .pipe(untilDestroyed(this))
    .subscribe((r) => {});
  }

  constructor(private abuseService: AbuseService) { }

  ngOnInit() {
  }

}
