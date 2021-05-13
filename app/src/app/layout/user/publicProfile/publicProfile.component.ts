import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { SortItem } from 'src/app/Dtos/sorting';
import { User } from 'src/app/Dtos/user';
import { ImageService } from 'src/app/services/image.service';
import { UserService } from 'src/app/services/user.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'app-publicProfile',
  templateUrl: './publicProfile.component.html',
  styleUrls: ['./publicProfile.component.scss'],
})
export class PublicProfileComponent implements OnInit {
  advertisements: Advertisement[] = [];
  user: User | null = null;
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
    limit: 12,
    offset: 0,
  };

  ownerPhone = '';

  formatPhone() {
    if (this.user?.phone != null && this.user?.phone.length == 12) {
      let tempPhone: string = this.user?.phone;
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

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private imageService: ImageService
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.userName = params['Username'];
    });
    this.loadUserInfo();

    window.scroll(0, 0);
  }

  loadUserInfo() {
    this.userService
      .getUser(this.userName)
      .pipe(untilDestroyed(this))
      .subscribe((data) => {
        this.user = data;
        this.formatPhone();
        if (this.user.avatar == null) {
          this.user.avatar = 'default';
        }

        this.imageService
          .getImageById(this.user.avatar)
          .pipe(untilDestroyed(this))
          .subscribe((data: any) => {
            this.user!.avatar = 'data:image/jpeg;base64,' + data.imageBlob;
          });

        this.queryParams = { ...this.queryParams, id: data.id };
        this.loadAdvertisements(this.queryParams);
      });
  }

  loadAdvertisements(queryParams: any) {
    this.userService
      .getUserAdvertisements(queryParams)
      .pipe(untilDestroyed(this))
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

  onPageChange(offset: number) {
    this.queryParams = { ...this.queryParams, offset };
    this.loadAdvertisements(this.queryParams);
  }

  onSort(orderBy: any) {
    this.queryParams = { ...this.queryParams, offset: 0, ...orderBy };
    this.loadAdvertisements(this.queryParams);
  }
}
