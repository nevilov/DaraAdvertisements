import { CookieService } from 'ngx-cookie-service';
import { UserService } from './../../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { switchMap } from 'rxjs/operators';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import { ImageService } from 'src/app/services/image.service';
import { ChatService } from '../../../services/chat.service';
import { NgDynamicBreadcrumbService } from 'ng-dynamic-breadcrumb';
import { ToastrService } from 'ngx-toastr';
import { FavoritesService } from 'src/app/services/favorites.service';
import { AppComponent } from 'src/app/app.component';

@UntilDestroy()
@Component({
  selector: 'app-advertisementDetailPage',
  templateUrl: './advertisementDetailPage.component.html',
  styleUrls: ['./advertisementDetailPage.component.scss'],
  providers: [AdvertisementService],
})
export class AdvertisementDetailPageComponent implements OnInit {
  id: number = 0;
  ownerPhone: string;
  advertisement: Advertisement;
  images: any[];
  imageValues: string[];
  userId: number = 0;
  userAvatar: string;
  categoryId: number = 0;
  isAuthors: boolean = false;
  deleteConfirmed: boolean = false;
  deleteText: string = 'Удалить объявление';
  sameAdvertisements: Advertisement[] | null = null;
  userAdvertisements: Advertisement[] | null = null;
  lat = 44.5950483;
  lon = 33.4758289;
  shareUrl: string = AppComponent.backendAddress;

  constructor(
    private route: ActivatedRoute,
    private cookieService: CookieService,
    private advertisementService: AdvertisementService,
    private imageService: ImageService,
    private chatService: ChatService,
    private router: Router,
    private userService: UserService,
    private favoritesService: FavoritesService,
    private toastr: ToastrService,
    private ngDynamicBreadcrumbService: NgDynamicBreadcrumbService
  ) {
    this.advertisement = {} as Advertisement;
    this.images = [];
    this.imageValues = [];
    this.userAvatar = '';
    this.ownerPhone = 'Телефон не указан';
  }

  changeImage(i: number) {
    this.imageValues[0] = this.imageValues[i];
  }

  formatPhone() {
    if (
      this.advertisement?.owner?.phone != null &&
      this.advertisement?.owner?.phone.length == 12
    ) {
      let tempPhone: string = this.advertisement?.owner?.phone;
      let newPhone: string =
        tempPhone[0] +
        tempPhone[1] +
        ' ' +
        tempPhone[2] +
        tempPhone[3] +
        tempPhone[4] +
        ' ' +
        tempPhone[5] +
        tempPhone[6] +
        tempPhone[7] +
        '-' +
        tempPhone[8] +
        tempPhone[9] +
        '-' +
        tempPhone[10] +
        tempPhone[11];
      this.ownerPhone = newPhone;
    }
  }

  ngOnInit() {
    this.route.paramMap
      .pipe(switchMap((params) => params.getAll('id')))
      .subscribe((data) => (this.id = +data));

    this.route.paramMap
      .pipe(switchMap((params) => params.getAll('categoryId')))
      .subscribe((data) => {
        this.categoryId = +data;
      });

    window.scroll(0, 0);

    this.advertisementService
      .getAdvertisementById(this.id)
      .pipe(untilDestroyed(this))
      .subscribe((data: Advertisement) => {
        this.advertisement = data;
        this.lat = this.advertisement.geoLat;
        this.lon = this.advertisement.geoLon;
        if (this.cookieService.get('UserId')) {
          if (this.advertisement.owner.id == this.cookieService.get('UserId')) {
            this.isAuthors = true;
          }
        }

        if (this.categoryId == 0) {
          this.categoryId = this.advertisement.category.id;
          this.router.navigateByUrl(
            'advertisements/' + this.categoryId + '/advertisement/' + this.id
          );
        }

        const breadcrumb = {
          category: this.advertisement.category.name,
          title: this.advertisement.title,
        };

        this.ngDynamicBreadcrumbService.updateBreadcrumbLabels(breadcrumb);

        if (this.advertisement.owner.avatar === null) {
          this.advertisement.owner.avatar = 'default';
        }

        this.imageService
          .getImageById(this.advertisement.owner.avatar)
          .pipe(untilDestroyed(this))
          .subscribe((data: any) => {
            this.userAvatar = 'data:image/jpeg;base64,' + data.imageBlob;
          });
        this.images = data.images;
        this.formatPhone();

        this.userService
          .getUserAdvertisementsWithLimit(this.advertisement.owner.id, 4, 0)
          .pipe(untilDestroyed(this))
          .subscribe((data) => {
            this.userAdvertisements = data.items;
            for (let i = 0; i < this.userAdvertisements.length; i++) {
              if (this.userAdvertisements[i].images[0] === undefined) {
                this.userAdvertisements[i].images[0] = { id: 'default' };
              }
            }
          });

        this.advertisementService
          .getSameAdvertisementsWithLimit(this.advertisement.category.id, 4)
          .pipe(untilDestroyed(this))
          .subscribe((data) => {
            this.sameAdvertisements = data.items;
            for (let i = 0; i < this.sameAdvertisements.length; i++) {
              if (this.sameAdvertisements[i].images[0] === undefined) {
                this.sameAdvertisements[i].images[0] = { id: 'default' };
              }
            }
          });

        for (let i = 0; i < this.images.length; i++) {
          this.imageService
            .getImageById(this.images[i].id)
            .pipe(untilDestroyed(this))
            .subscribe((data: any) => {
              this.imageValues[i + 1] =
                'data:image/jpeg;base64,' + data.imageBlob;
              if (i == 0) {
                this.imageValues[0] = this.imageValues[1];
              }
            });
        }
      });
  }

  addToFavorites() {
    this.favoritesService
      .addToFavorites(this.id)
      .pipe(untilDestroyed(this))
      .subscribe(
        (response) => this.toastr.success('', 'Добавлено в избранное!'),
        (error) => this.toastr.error(error.error.error, 'Ошибка!')
      );
  }

  deleteFromFavorites() {
    this.favoritesService
      .deleteFromFavorites(this.id)
      .pipe(untilDestroyed(this))
      .subscribe();
    this.toastr.success('', 'Удалено из избранного!');
  }

  onEditClicked() {
    this.router.navigateByUrl('/editAdvertisement/' + this.id);
  }

  onDeleteClicked() {
    if (this.deleteConfirmed) {
      this.advertisementService.deleteAdvertisement(this.id).subscribe((r) => {
        this.router.navigateByUrl('/');
      });
    } else {
      this.deleteText = 'Подтвердить удаление?';
      this.deleteConfirmed = true;
    }
  }

  onCreateChat() {
    this.chatService.createChat(this.id).subscribe();
    return this.router.navigateByUrl('/chats');
  }
}
