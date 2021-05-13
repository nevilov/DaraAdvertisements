import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { CookieService } from 'ngx-cookie-service';
import { SortItem } from 'src/app/Dtos/sorting';
import { ImageService } from 'src/app/services/image.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import { ToastrService } from 'ngx-toastr';

@UntilDestroy()
@Component({
  selector: 'app-userProfileAdvertisements',
  templateUrl: './userProfileAdvertisements.component.html',
  styleUrls: ['./userProfileAdvertisements.component.scss'],
})
export class UserProfileAdvertisementsComponent implements OnInit {
  advertisements: Advertisement[] = [];
  userName: string = '';

  sotringElements: SortItem[] = [
    {
      title: 'По умолчанию',
      orderBy: {
        sortBy: 'Id',
        sortDirection: 'asc',
      },
    },
    {
      title: 'Дешевле',
      orderBy: {
        sortBy: 'Price',
        sortDirection: 'asc',
      },
    },
    {
      title: 'Дороже',
      orderBy: {
        sortBy: 'Price',
        sortDirection: 'desc',
      },
    },
    {
      title: 'Сначала новые',
      orderBy: {
        sortBy: 'CreatedDate',
        sortDirection: 'desc',
      },
    },
    {
      title: 'Сначала старые',
      orderBy: {
        sortBy: 'CreatedDate',
        sortDirection: 'asc',
      },
    },
  ];

  total: number = 0;

  queryParams = {
    id: '',
    limit: 10,
    offset: 0,
  };

  constructor(
    private userService: UserService,
    private imageService: ImageService,
    private cookieService: CookieService,
    private advertisementService: AdvertisementService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.userName = this.cookieService.get('AuthUsername');
    this.loadUserInfo();
  }

  loadAdvertisements(queryParams: any) {
    this.userService.getUserAdvertisements(queryParams).subscribe((data) => {
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

  loadUserInfo() {
    this.userService
      .getUser(this.userName)
      .pipe(untilDestroyed(this))
      .subscribe((data) => {
        this.queryParams = { ...this.queryParams, id: data.id };
        this.loadAdvertisements(this.queryParams);
      });
  }

  onRemove(id: number, event: any) {
    event.stopPropagation();
    event.preventDefault();
    let isConfirmed = confirm('Вы действивтельно хотите удалить объявление?');

    if (isConfirmed) {
      this.advertisementService
        .deleteAdvertisement(id)
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          this.loadUserInfo();
        });
      this.toastr.success('', 'Успешно удалено!');
    }
  }

  onPageChange(offset: number) {
    this.queryParams = { ...this.queryParams, offset };
    this.loadAdvertisements(this.queryParams);
  }

  onSort(orderBy: any) {
    this.queryParams = { ...this.queryParams, offset: 0, ...orderBy };
    this.loadAdvertisements(this.queryParams);
  }
}
