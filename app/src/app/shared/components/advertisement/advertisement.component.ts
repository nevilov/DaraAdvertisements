import { Component, Input, OnInit } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-advertisement',
  templateUrl: './advertisement.component.html',
  styleUrls: ['./../../../../assets/scss/layout/__advertisement.scss'],
})
@UntilDestroy()
export class AdvertisementComponent implements OnInit {
  @Input() title: string = '';
  @Input() price: number = 0;
  @Input() cover: string = '';
  @Input() ownerName: string = 'Доменный Юзер';
  @Input() ownerLogin: string = 'ДоменныйЮзер';
  @Input() ownerId: string = ''; // Решение на скорую руку TODO Исправить
  @Input() avatar: string = '';
  @Input() createdDate: Date = new Date();
  @Input() routLink: number = 0;
  @Input() thisUserAdvertisement: boolean = false;
  imageBlob: string = '';
  avatarBlob: string = '';

  constructor(private imageService: ImageService) {}

  ngOnInit() {
    this.imageService
      .getImageById(this.cover)
      .pipe(untilDestroyed(this))
      .subscribe((data: any) => {
        if (data.imageBlob) {
          this.imageBlob = 'data:image/jpeg;base64,' + data.imageBlob;
        }
      });

    if (this.avatar == 'null') {
      this.avatar = 'default';
    }
    this.imageService
      .getImageById(this.avatar)
      .pipe(untilDestroyed(this))
      .subscribe((data: any) => {
        if (data.imageBlob) {
          this.avatarBlob = 'data:image/jpeg;base64,' + data.imageBlob;
        }
      });
  }
}
