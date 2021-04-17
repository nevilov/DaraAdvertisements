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

  closeAbuse(clickedId: number, clickedOrder: number): void {
    console.log("close abuse " + clickedId + " clicked");
    this.abuseService.closeAbuse(clickedId)
    .pipe(untilDestroyed(this))
    .subscribe(() => console.log('Delete successful'));
    this.abuseItems.splice(clickedOrder, 1);
  }

  ngOnInit(): void {
    this.abuseService.getAbuses()
    .pipe(untilDestroyed(this))
    .subscribe(data => {
      this.abuseItems = data.items.filter((abs) => (abs.removedDate == null));
    });
  }


}
