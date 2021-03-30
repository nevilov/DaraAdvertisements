import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Abuse, NewAbuse } from 'src/app/Dtos/abuse';
import { AbuseService } from './../../../services/abuse.service';

@UntilDestroy()
@Component({
  selector: 'app-abusePage',
  templateUrl: './abusePage.component.html',
  styleUrls: ['./abusePage.component.scss']
})
export class AbusePageComponent implements OnInit {

  abuseItems: Abuse[] = [];

  constructor(private abuseService: AbuseService) { 
  }

  ngOnInit(): void {
    this.abuseService.getAbuses()
    .pipe(untilDestroyed(this))
    .subscribe(data => {
      this.abuseItems = data.items;
    });
  }


}
