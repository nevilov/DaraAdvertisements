import { Component, Input, OnInit } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-advertisement',
  templateUrl: './advertisement.component.html',
  styleUrls: ['./advertisement.component.scss'],
})

@UntilDestroy()
export class AdvertisementComponent implements OnInit {
  @Input() title: string = '';
  @Input() price: number = 0;
  @Input() cover: string = '';
  @Input() ownerName: string = 'Доменный Юзер';
  @Input() createdDate: Date = new Date();
  imageBlob: string;

  constructor(
    private imageService: ImageService
  ) {
    this.imageBlob = 'data:image/jpeg;base64, no_image';
  }

  ngOnInit() {
    this.imageService.getImageById(this.cover)
    .pipe(untilDestroyed(this))
    .subscribe((data: any) => {
      if (data.imageBlob) {
          this.imageBlob = 'data:image/jpeg;base64,' + data.imageBlob;
      }
    });
  }
}
