import { NULL_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { switchMap } from 'rxjs/operators';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import { ImageService } from 'src/app/services/image.service';

@UntilDestroy()

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
    ownerPhone: string;
    advertisement: Advertisement;
    images: any[];
    imageValues: string[];


    constructor(
      private route: ActivatedRoute,
      private advertisementService: AdvertisementService,
      private imageService: ImageService
      ){
      this.advertisement = {} as Advertisement;
      this.images = [];
      this.imageValues = [];
      this.ownerPhone = "Не указан";
    }

    changeImage(i: number) {
      this.imageValues[0] = this.imageValues[i];
    }

    formatPhone() {
      if (this.advertisement?.owner?.phone != null && this.advertisement?.owner?.phone.length == 12 ) {
        let tempPhone:string = this.advertisement?.owner?.phone;
        let newPhone:string = tempPhone[0] + tempPhone[1] + " " + tempPhone[2] + tempPhone[3] + tempPhone[4] + " " + tempPhone[5] + tempPhone[6] + tempPhone[7] + "-" + tempPhone[8] + tempPhone[9] + "-" + tempPhone[10] + tempPhone[11];
        this.ownerPhone = newPhone;
      }
    }

    ngOnInit() {
        window.scrollTo(0, 0);
        this.route.paramMap.pipe(
            switchMap(params => params.getAll('id'))
        )
        .pipe(untilDestroyed(this))
        .subscribe(data=> this.id = +data);

        this.advertisementService.getAdvertisementById(this.id)
            .subscribe((data: Advertisement) => {
              this.advertisement = data;
              this.images = data.images;
//              console.log(this.advertisement);
              this.formatPhone();

              for (let i = 0; i < this.images.length; i++) {
                this.imageService.getImageById(this.images[i].id)
                .pipe(untilDestroyed(this))
                .subscribe((data: any) => {
                  this.imageValues[i + 1] = 'data:image/jpeg;base64,' + data.imageBlob;
                  if (i == 0) {
                    this.imageValues[0] = this.imageValues[1];
                  }
                });
              }
            });
            
    }
}
