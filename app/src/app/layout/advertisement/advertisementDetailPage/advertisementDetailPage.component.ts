import { NULL_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';

@Component({
  selector: 'app-advertisementDetailPage',
  templateUrl: './advertisementDetailPage.component.html',
  styleUrls: ['./advertisementDetailPage.component.scss'],
  providers: [
    AdvertisementService
  ]
})
export class AdvertisementDetailPageComponent implements OnInit {

    id: number = 0;
    advertisement: Advertisement | null = null;

    constructor(private route: ActivatedRoute, private advertisementService: AdvertisementService){

    }

    ngOnInit() {
        this.route.paramMap.pipe(
            switchMap(params => params.getAll('id'))
        )
        .subscribe(data=> this.id = +data);

        this.advertisementService.getAdvertisementById(this.id)
            .subscribe((data: Advertisement) => {
              this.advertisement = data;
              console.log(data)
            });
    }
}
