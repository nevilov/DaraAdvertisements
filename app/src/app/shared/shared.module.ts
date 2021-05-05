import { ReactiveFormsModule } from '@angular/forms';
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

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    NgDynamicBreadcrumbModule,
  ],
  declarations: [
    FooterComponent,
    MenuComponent,
    SubmenuComponent,
    BreadcrumbsComponent,
    AdvertisementComponent,
    PaginationControlComponent,
  ],
  exports: [
    FooterComponent,
    MenuComponent,
    SubmenuComponent,
    BreadcrumbsComponent,
    AdvertisementComponent,
    PaginationControlComponent,
  ],
})
export class SharedModule {}
