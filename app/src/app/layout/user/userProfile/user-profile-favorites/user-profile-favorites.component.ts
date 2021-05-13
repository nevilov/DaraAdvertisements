import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { FavoritesService } from 'src/app/services/favorites.service';
import { ImageService } from 'src/app/services/image.service';

@UntilDestroy()
@Component({
  selector: 'app-user-profile-favorites',
  templateUrl: './user-profile-favorites.component.html',
  styleUrls: ['./user-profile-favorites.component.scss'],
})
export class UserProfileFavoritesComponent implements OnInit {
  advertisements: Advertisement[] = [];

  total: number = 0;

  queryParams = {
    limit: 10,
    offset: 0,
  };

  constructor(
    private favoritesService: FavoritesService,
    private imageService: ImageService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.loadAdvertisements(this.queryParams);
  }

  loadAdvertisements(queryParams: any) {
    this.favoritesService
      .getFavorites(queryParams)
      .pipe(untilDestroyed(this))
      .subscribe((data) => {
        this.advertisements = data.items;
        this.total = data.total;

        for (let i = 0; i < this.advertisements.length; i++) {
          if (this.advertisements[i].images[0] === undefined) {
            this.advertisements[i].images[0] = { id: 'default' };
          }

          this.imageService
            .getImageById(this.advertisements[i].images[0].id)
            .pipe(untilDestroyed(this))
            .subscribe((data: any) => {
              if (data.imageBlob) {
                this.advertisements[i].images[0] =
                  'data:image/jpeg;base64,' + data.imageBlob;
              }
            });
        }
      });
  }

  onRemove(id: number, event: any) {
    event.stopPropagation();
    event.preventDefault();

    this.favoritesService
      .deleteFromFavorites(id)
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.loadAdvertisements(this.queryParams);
      });
    this.toastr.success('', 'Удалено из избранного.');
  }

  onPageChange(offset: number) {
    this.queryParams = { ...this.queryParams, offset };
    this.loadAdvertisements(this.queryParams);
  }
}
