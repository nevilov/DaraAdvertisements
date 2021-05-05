import { switchMap, catchError } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';

@Component({
  selector: 'app-advertisementPage',
  templateUrl: './advertisementPage.component.html',
  styleUrls: ['./../../../../assets/scss/pages/__home.scss'],
  providers: [AdvertisementService],
})
export class AdvertisementPageComponent implements OnInit {
  advertisements: Advertisement[] = [];

  total: number = 0;
  offset: number = 0;
  limit: number = 10;

  constructor(
    private route: ActivatedRoute,
    private advertisementService: AdvertisementService
  ) {}

  ngOnInit() {
    this.loadAdvertisements(this.limit, this.offset);
  }

  loadAdvertisements(limit: number, offset: number) {
    this.advertisementService
      .getAllAdvertisements(limit, offset)
      .subscribe((data) => {
        this.advertisements = data.items;
        this.total = data.total;

        for (let i = 0; i < this.advertisements.length; i++) {
          if (this.advertisements[i].images[0] === undefined) {
            this.advertisements[i].images[0] = { id: 'default' };
          }
        }
      });
  }

  loadAdvertisementsByCategory(categoryId: number) {
    this.advertisementService
      .getAdvertisementsByCategoryId(categoryId)
      .subscribe((data) => {
        this.advertisements = data.items;

        for (let i = 0; i < this.advertisements.length; i++) {
          if (this.advertisements[i].images[0] === undefined) {
            this.advertisements[i].images[0] = { id: 'default' };
          }
        }
      });
  }

  onPageChange(offset: number) {
    this.offset = offset;
    console.log(offset);
    this.loadAdvertisements(this.limit, this.offset);
  }
}
