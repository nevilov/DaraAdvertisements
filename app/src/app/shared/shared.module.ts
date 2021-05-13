import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './components/footer/footer.component';
import { MenuComponent } from './components/menu/menu.component';
import { SubmenuComponent } from './components/submenu/submenu.component';
import { BreadcrumbsComponent } from './components/breadcrumbs/breadcrumbs.component';
import { AdvertisementComponent } from './components/advertisement/advertisement.component';
import { NgDynamicBreadcrumbModule } from 'ng-dynamic-breadcrumb';
import { PaginationControlComponent } from './components/pagination-control/pagination-control.component';
import { SearchComponent } from './components/search/search.component';
import { SortComponent } from './components/sort/sort.component';
import { FilterComponent } from './components/filter/filter.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    NgDynamicBreadcrumbModule,
  ],
  declarations: [
    FooterComponent,
    MenuComponent,
    SubmenuComponent,
    BreadcrumbsComponent,
    AdvertisementComponent,
    PaginationControlComponent,
    SearchComponent,
    SortComponent,
    FilterComponent,
  ],
  exports: [
    FooterComponent,
    MenuComponent,
    SubmenuComponent,
    BreadcrumbsComponent,
    AdvertisementComponent,
    PaginationControlComponent,
    SearchComponent,
    SortComponent,
    FilterComponent,
  ],
})
export class SharedModule {}
