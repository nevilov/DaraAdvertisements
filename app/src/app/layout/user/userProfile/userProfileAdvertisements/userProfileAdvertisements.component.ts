import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { CookieService } from 'ngx-cookie-service';
import { SortItem } from 'src/app/Dtos/sorting';

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
    private cookieService: CookieService
  ) {}

  ngOnInit() {
    this.userName = this.cookieService.get('AuthUsername');
    this.loadUserInfo();
  }

  loadAdvertisements(queryParams: any) {
    this.userService.getUserAdvertisements(queryParams).subscribe((data) => {
      this.advertisements = data.items;
      this.total = data.total;
      console.log(data);
    });
  }

  loadUserInfo() {
    this.userService.getUser(this.userName).subscribe((data) => {
      this.queryParams = { ...this.queryParams, id: data.id };
      this.loadAdvertisements(this.queryParams);
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
