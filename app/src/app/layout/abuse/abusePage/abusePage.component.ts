import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ToastrService } from 'ngx-toastr';
import { Abuse } from 'src/app/Dtos/abuse';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import { AbuseService } from './../../../services/abuse.service';

@UntilDestroy()
@Component({
  selector: 'app-abusePage',
  templateUrl: './abusePage.component.html',
  styleUrls: ['./abusePage.component.scss'],
})
export class AbusePageComponent implements OnInit {
  abuseItems: Abuse[] = [];

  total: number = 0;
  queryParams = {
    limit: 10,
    offset: 0,
  };

  constructor(
    private abuseService: AbuseService,
    private advertisementService: AdvertisementService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getAllAbuse(this.queryParams);
  }

  getAllAbuse(queryParams: any) {
    this.abuseService
      .getAbuses(queryParams)
      .pipe(untilDestroyed(this))
      .subscribe((data) => {
        this.abuseItems = data.items;
        this.total = data.total;
      });
  }

  closeAbuse(clickedId: number, event: any): void {
    event.preventDefault();
    this.abuseService
      .closeAbuse(clickedId)
      .pipe(untilDestroyed(this))
      .subscribe((response) => {
        this.toastr.success('', 'Удалено!');
        this.getAllAbuse(this.queryParams);
      });
  }

  onCheckAdvertisement(id: number) {
    this.advertisementService.getAdvertisementById(id).subscribe((response) => {
      console.log(response);

      this.router.navigateByUrl(
        `advertisements/${response.category.id}/advertisement/${id}`
      );
    });
  }
  onPageChange(offset: number) {
    this.queryParams = { ...this.queryParams, offset };
    this.getAllAbuse(this.queryParams);
  }
}
