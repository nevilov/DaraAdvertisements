import { ActivatedRoute, Router } from '@angular/router';
import { Category } from 'src/app/Dtos/category';
import { CategoryService } from './../../../services/category.service';
import { AdvertisementService } from 'src/app/services/advertisements.service';
import { Component, OnInit } from '@angular/core';
import { Advertisement } from 'src/app/Dtos/advertisement';
import { NgDynamicBreadcrumbService } from 'ng-dynamic-breadcrumb';
import { switchMap } from 'rxjs/operators';

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

    if (this.categoryId == -1) {
      this.loadAdvertisements();
    } else {
      this.loadAdvertisementsByCategory(this.categoryId);
    }
  }

  loadAdvertisementsByCategory(categoryId: number) {
    this.advertisementService
      .getAdvertisementsByCategoryId(categoryId)
      .subscribe((data) => {
        this.advertisements = data.items;
        console.log(this.advertisements);
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

      console.log(this.categories);

      const breadcrumb = { categoryName: this.categories.name };
      this.ngDynamicBreadcrumbService.updateBreadcrumbLabels(breadcrumb);
    });
  }

  loadAdvertisements() {
    this.advertisementService.getAllAdvertisements(10, 0).subscribe((data) => {
      this.advertisements = data.items;
      console.log(this.advertisements);
    });
  }
}
