import { ActivatedRoute, Router } from '@angular/router';
import { Category } from 'src/app/Dtos/category';
import { CategoryService } from './../../../services/category.service';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import { Component, OnInit } from '@angular/core';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { NgDynamicBreadcrumbService } from 'ng-dynamic-breadcrumb';
import { switchMap } from 'rxjs/operators';
import { SortItem } from 'src/app/Dtos/sorting';

@Component({
  selector: 'app-advertisementPageWithSubCategories',
  templateUrl: './advertisementPageWithSubCategories.component.html',
  styleUrls: ['./advertisementPageWithSubCategories.component.scss'],
  providers: [AdvertisementService],
})
export class AdvertisementPageWithSubCategoriesComponent implements OnInit {
  advertisements: Advertisement[] = [];
  categories: Category | null = null;
  categoryId = -1;

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
    limit: 10,
    offset: 0,
    searchString: '',
    categoryId: 0,
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private categoryService: CategoryService,
    private advertisementService: AdvertisementService,
    private ngDynamicBreadcrumbService: NgDynamicBreadcrumbService
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit() {
    this.route.paramMap
      .pipe(switchMap((params) => params.getAll('categoryId')))
      .subscribe((data) => {
        this.categoryId = +data;
        this.getCategoriesById(this.categoryId);
      });

    if (this.categoryId !== -1) {
      this.queryParams = { ...this.queryParams, categoryId: this.categoryId };
    }
    this.loadAdvertisements(this.queryParams);
  }
  //TODO Можно сносить - работает без него
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

  public getCategoriesById(id: number) {
    this.categoryService.getCategoryChildrens(id).subscribe((data) => {
      this.categories = data.parent;

      const breadcrumb = { categoryName: this.categories.name };
      this.ngDynamicBreadcrumbService.updateBreadcrumbLabels(breadcrumb);
    });
  }

  loadAdvertisements(queryParams: any) {
    this.advertisementService
      .getAllAdvertisements(queryParams)
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

  onSearch(searchString: string) {
    this.queryParams = { ...this.queryParams, offset: 0, searchString };
    this.loadAdvertisements(this.queryParams);
  }

  onSort(orderBy: any) {
    this.queryParams = { ...this.queryParams, offset: 0, ...orderBy };
    this.loadAdvertisements(this.queryParams);
  }

  onFilter(filterParams: object) {
    this.queryParams = { ...this.queryParams, offset: 0, ...filterParams };
    this.loadAdvertisements(this.queryParams);
  }
}
